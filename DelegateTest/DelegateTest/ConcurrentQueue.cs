﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateTest
{
    public class ConcurrentQueue
    {
        public static void DoThis()
        {
            Task t = RunProgram();
            t.Wait();
            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            var taskQueue = new ConcurrentQueue<CustomTask>();
            var cts = new CancellationTokenSource();
            // 生成任务添加至并发队列
            var taskSource = Task.Run(() => TaskProducer(taskQueue));
            // 同时启动四个任务处理队列中的任务
            Task[] processors = new Task[4];
            for (int i = 1; i <= 4; i++)
            {
                string processId = i.ToString();
                processors[i - 1] = Task.Run(() => TaskProcessor(taskQueue, "Processor" + processId, cts.Token));
            }
            await taskSource;
            // 向任务发送取消信号
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            await Task.WhenAll(processors);
        }

        private static async Task TaskProducer(ConcurrentQueue<CustomTask> queue)
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(50);
                var workItem = new CustomTask { Id = i };
                queue.Enqueue(workItem);
                Console.WriteLine("task {0} has been posted", workItem.Id);
            }
        }

        static async Task TaskProcessor(ConcurrentQueue<CustomTask> queue, string name, CancellationToken token)
        {
            CustomTask workItem;
            bool dequeueSuccessful = false;
            await GetRandomDelay();
            do
            {
                dequeueSuccessful = queue.TryDequeue(out workItem);
                if (dequeueSuccessful)
                {
                    Console.WriteLine("task {0} has been processed by {1}", workItem.Id, name);
                }
                await GetRandomDelay();
            }
            while (!token.IsCancellationRequested);
        }

        private static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1500);
            return Task.Delay(delay);
        }
    }

    class CustomTask
    {
        public int Id { get; set; }
    }
}
