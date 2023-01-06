using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Zhai.Famil.Common.Collections
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random _local;

        public static Random ThisThreadsRandom => _local ?? (_local = new Random(unchecked(Environment.TickCount * 31 + Environment.CurrentManagedThreadId)));
    }

    public static class ConcurrentObservableCollectionExtensions
    {
        /// <summary>
        /// 随机排序
        /// </summary>
        /// <param name="list"></param>
        public static void Shuffle<T>(this ConcurrentObservableCollection<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// 批量装载
        /// </summary>
        /// <returns></returns>
        public static async Task BatchLoadAsync<T>(this ConcurrentObservableCollection<T> list, IEnumerable<T> collection, CancellationTokenSource cancellationTokenSource = null)
        {
            if (!collection.Any()) return;

            await Task.Delay(300);

            foreach (var item in collection)
            {
                if (cancellationTokenSource != null && cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                list.Add(item);
            }
        }

        /// <summary>
        /// 分批装载
        /// </summary>
        /// <returns></returns>
        public static async Task BatchSplitLoadAsync<T>(this ConcurrentObservableCollection<T> list, IEnumerable<T> collection, int splitLength = 20, CancellationTokenSource cancellationTokenSource = null)
        {
            if (!collection.Any()) return;

            var batchNumber = Math.Ceiling((double)collection.Count() / (double)splitLength);

            for (var i = 0; i < batchNumber; i++)
            {
                if (cancellationTokenSource != null && cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                var batchList = collection.Skip(i * splitLength).Take(splitLength);

                await Task.Delay(300);

                foreach (var item in batchList)
                {
                    if (cancellationTokenSource != null && cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    list.Add(item);
                }
            }
        }
    }
}