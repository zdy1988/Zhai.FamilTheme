using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using Zhai.Famil.Common.Collections;
using Zhai.Famil.Common.Threads;

/// <summary>
/// 表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知。支持跨线程和并发更新。.Net 4.5后可用。
/// </summary>
/// <typeparam name="T">集合中的元素类型。</typeparam>
[Serializable]
public class ConcurrentObservableCollection<T> : ObservableCollection<T>
{
    /// <summary>
    /// 定义线程同步上下文。
    /// </summary>
    private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

    /// <summary>
    /// 定义同步锁对象。
    /// </summary>
    private readonly UsingLock<object> _syncLock = new UsingLock<object>();

    /// <summary>
    /// 在集合发生改变时发生。
    /// </summary>
    private NotifyCollectionChangedEventHandler _collectionChanged;

    /// <summary>
    /// Defines the _isObservableSuspended
    /// </summary>
    private bool _isObservableSuspended;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrentObservableCollection{T}" /> class.
    /// </summary>
    public ConcurrentObservableCollection()
    {
        using (_syncLock.Read())
        {
            EnableCollectionSynchronization(this, _syncLock);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrentObservableCollection{T}" /> class.
    /// </summary>
    /// <param name="list">集合。</param>
    public ConcurrentObservableCollection(IEnumerable<T> list) : base(list)
    {
        using (_syncLock.Read())
        {
            EnableCollectionSynchronization(this, _syncLock);
        }
    }

    /// <summary>
    /// 使集合同步。
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="lockObject"></param>
    private static void EnableCollectionSynchronization(IEnumerable collection, object lockObject)
    {
        var method = typeof(BindingOperations).GetMethod("EnableCollectionSynchronization",
            new[] { typeof(IEnumerable), typeof(object) });
        if (method != null)
            // It's .NET 4.5
            method.Invoke(null, new[] { collection, lockObject });
    }

    /// <summary>
    /// 获取集合中实际包含的元素数。
    /// </summary>
    public new int Count
    {
        get
        {
            using (_syncLock.Read())
            {
                return base.Count;
            }
        }
    }

    /// <summary>
    /// 获取集合中实际包含的元素数(未加锁)
    /// </summary>
    public int FastCount
    {
        get
        {
            return Items.Count;
        }
    }

    /// <summary>
    /// 根据索引获取项。
    /// </summary>
    /// <param name="index">索引。</param>
    /// <returns></returns>
    public new T this[int index]
    {
        get
        {
            using (_syncLock.Read())
            {
                var t = base[index];
                return t;
            }
        }
        set
        {
            using (_syncLock.Write())
            {
                base[index] = value;
            }
        }
    }

    /// <summary>
    /// Creates a shallow copy of a range of elements in the source List(Of T).
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="count">The count.</param>
    /// <returns></returns>
    public List<T> GetRange(int index, int count)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException("index");
        if (count < 0)
            throw new ArgumentOutOfRangeException("index");
        if (Count - index < count)
            throw new ArgumentException("Invalid Offset Length");

        return new List<T>(Items.Skip(index).Take(count));
    }

    /// <summary>
    /// 将对象添加到集合的结尾处。
    /// </summary>
    /// <param name="item">The <see cref="T" /></param>
    public new void Add(T item)
    {
        void DoAdd()
        {
            using (_syncLock.Write())
            {
                base.Add(item);
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoAdd);
    }

    /// <summary>
    /// 批量将对象添加到集合
    /// </summary>
    /// <param name="collection">The collection.</param>
    public void AddRange(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException("collection");

        if (!collection.Any())
            return;

        _isObservableSuspended = true;

        try
        {
            if (Items != null)
                using (var enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        Items.Add(enumerator.Current);
                }
        }
        finally
        {
            _isObservableSuspended = false;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    /// <summary>
    /// Adds the elements of the specified collection to the end of the
    ///     <see cref="System.Collections.ObjectModel.ObservableCollection&lt;T&gt;" />.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="clearExistingData">if set to <c>true</c> [clear existing data].</param>
    public void AddRange(IEnumerable<T> collection, bool clearExistingData)
    {
        if (clearExistingData)
        {
            _isObservableSuspended = true;
            Clear();
        }

        AddRange(collection);
    }

    /// <summary>
    /// 将元素插入集合的指定索引处。
    /// </summary>
    /// <param name="index">The <see cref="int " /></param>
    /// <param name="item">The <see cref="T" /></param>
    public new void Insert(int index, T item)
    {
        void DoInsert()
        {
            using (_syncLock.Write())
            {
                base.Insert(index, item);
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoInsert);
    }

    /// <summary>
    /// Inserts the elements of the specified collection to the specified index of the
    ///     <see cref="System.Collections.ObjectModel.ObservableCollection&lt;T&gt;" />.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="collection">The collection.</param>
    public void InsertRange(int index, IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException("collection");
        if (index < 0 || index > Count)
            throw new IndexOutOfRangeException();

        _isObservableSuspended = true;
        var startIndex = index;

        try
        {
            if (Items != null)
                using (var enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Items.Insert(startIndex, enumerator.Current);
                        startIndex++;
                    }
                }
        }
        finally
        {
            _isObservableSuspended = false;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    /// <summary>
    /// 从集合中移除特定对象的第一个匹配项。
    /// </summary>
    /// <param name="item">The <see cref="T" /></param>
    public new void Remove(T item)
    {
        void DoRemove()
        {
            using (_syncLock.Write())
            {
                base.Remove(item);
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoRemove);
    }

    /// <summary>
    /// Removes a range of elements specified in the collection from the
    ///     <see cref="System.Collections.ObjectModel.ObservableCollection&lt;T&gt;" />.
    /// </summary>
    /// <param name="collection">The collection.</param>
    public void RemoveRange(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException("collection");

        _isObservableSuspended = true;

        try
        {
            if (Items != null)
            {
                using (IEnumerator<T> enumerator = new List<T>(collection).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        Items.Remove(enumerator.Current);
                }
            }
        }
        finally
        {
            _isObservableSuspended = false;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    /// <summary>
    /// 移除集合的指定索引处的元素。
    /// </summary>
    /// <param name="index">The <see cref="int " /></param>
    public new void RemoveAt(int index)
    {
        void DoRemoveAt()
        {
            using (_syncLock.Write())
            {
                base.RemoveAt(index);
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoRemoveAt);
    }

    /// <summary>
    /// 将集合替换成只有一个指定元素
    /// </summary>
    /// <param name="item"></param>
    public void Replace(T item)
    {
        ReplaceRange(new[] { item });
    }

    /// <summary>
    /// 将集合替换为另一个集合
    /// </summary>
    /// <param name="collection"></param>
    public void ReplaceRange(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException("collection");

        using (_syncLock.Write())
        {
            Clear();

            foreach (var i in collection)
            {
                Insert(base.Count - 1, i);
            }
        }
    }

    /// <summary>
    /// 将对象添加到集合的结尾处。
    /// </summary>
    public new void Clear()
    {
        void DoClear()
        {
            using (_syncLock.Write())
            {
                base.Clear();
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoClear);
    }

    /// <summary>
    /// 刷新集合中的某个对象
    /// </summary>
    /// <param name="item"></param>
    public void Refresh(T item)
    {
        void DoRefresh()
        {
            var index = IndexOf(item);

            using (_syncLock.Write())
            {

                base.RemoveAt(index);

                base.Insert(index, item);
            }
        }

        ApplicationDispatcher.InvokeOnUIThread(DoRefresh);
    }

    /// <summary>
    /// 搜索指定的对象，并返回整个集合中第一个匹配项的从零开始的索引。
    /// </summary>
    /// <param name="item">The <see cref="T" /></param>
    /// <returns>The <see cref="int " /></returns>
    public new int IndexOf(T item)
    {
        using (_syncLock.Read())
        {
            var index = base.IndexOf(item);
            return index;
        }
    }

    /// <summary>
    /// 确定某元素是否在集合中。
    /// </summary>
    /// <param name="item">The <see cref="T" /></param>
    /// <returns>The <see cref="bool " /></returns>
    public new bool Contains(T item)
    {
        using (_syncLock.Read())
        {
            var result = base.Contains(item);
            return result;
        }
    }

    /// <summary>
    /// 从目标数组的指定索引处开始将整个集合复制到兼容的一维数组中。
    /// </summary>
    /// <param name="array">The <see cref="T [ ]" /></param>
    /// <param name="arrayIndex">The <see cref="int " /></param>
    public new void CopyTo(T[] array, int arrayIndex)
    {
        using (_syncLock.Write())
        {
            base.CopyTo(array, arrayIndex);
        }
    }

    /// <summary>
    /// 拷贝集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> Clone()
    {
        return new ConcurrentObservableCollection<T>(this.ToList());
    }

    /// <summary>
    /// 返回循环访问集合的枚举数。
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}" /></returns>
    public new IEnumerator<T> GetEnumerator()
    {
        using (_syncLock.Read())
        {
            var enumerator = base.GetEnumerator();
            return enumerator;
        }
    }

    /// <summary>
    /// 在集合发生改变时发生。
    /// </summary>
    public override event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add => _collectionChanged += value;
        remove => _collectionChanged -= value;
    }

    /// <summary>
    /// 重写集合发生变化事件处理函数。
    /// </summary>
    /// <param name="e">集合变化事件参数。</param>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (_isObservableSuspended) return;

        // 不允许可重入的更改此集合的尝试。
        using (BlockReentrancy())
        {
            if (_collectionChanged != null)
            {
                var delegates = _collectionChanged.GetInvocationList();

                // 遍历调用列表。
                foreach (var @delegate in delegates)
                {
                    var handler = (NotifyCollectionChangedEventHandler)@delegate;
                    // 如果订阅者是DispatcherObject和不同的线程。
                    if (handler != null)
                        // 目标调度程序线程中的调用处理程序。
                        handler.Invoke(handler, e);
                    else // 按原样执行处理程序。
                        _collectionChanged(this, e);
                }
            }
        }
    }

    /// <summary>
    /// 重写属性发生改变事件处理函数。
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (!_isObservableSuspended) return;

        if (SynchronizationContext.Current == _synchronizationContext)
            //如果操作发生在UI线程中，不需要进行跨线程执行
            RaisePropertyChanged(e);
        else
            // 如果操作发生在非UI线程中
            _synchronizationContext?.Post(RaisePropertyChanged, e);
    }

    /// <summary>
    /// 触发属性改变事件。
    /// </summary>
    /// <param name="param"></param>
    private void RaisePropertyChanged(object param)
    {
        base.OnPropertyChanged((PropertyChangedEventArgs)param);
    }

    /// <summary>
    /// Resumes the notifications.
    /// </summary>
    public void ResumeNotifications()
    {
        _isObservableSuspended = false;
    }

    /// <summary>
    /// Suspends the notifications.
    /// </summary>
    public void SuspendNotifications()
    {
        _isObservableSuspended = true;
    }

    /// <summary>
    /// 将list1与list2组合，并返回修改后的list1。
    /// </summary>
    /// <param name="list1">第一个集合</param>
    /// <param name="list2">第二个集合</param>
    /// <returns>修改后的list1实例</returns>
    public static ConcurrentObservableCollection<T> operator +(ConcurrentObservableCollection<T> list1, IList<T> list2)
    {
        foreach (var item in list2)
            list1.Add(item);
        return list1;
    }
}