using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace Collection.NUnitTests
{
    [TestFixture]
    public class QueueTests
    {
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void CountTest(int[] array)
        {
            var queue = new Queue<int>(array);
            Assert.AreEqual(array.Length, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void CopyToTest(int[] array)
        {
            var queue = new Queue<int>(array);
            var temp1 = new int[array.Length];
            queue.CopyTo(temp1, 0);
            Assert.IsTrue(array.SequenceEqual(temp1));

            Array temp2 = new int[array.Length];
            var tempQueue = (ICollection)queue;
            tempQueue.CopyTo(temp2, 0);
            int i = 0;
            foreach (var element in temp2)
            {
                Assert.AreEqual(array[i++], element);
            }
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void CopyToExceptionTest(int[] array)
        {
            var queue = new Queue<int>(array);
            var temp1 = new int[0];
            try
            {
                queue.CopyTo(temp1, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
            }

            try
            {
                temp1 = new int[array.Length];
                queue.CopyTo(temp1, -1);
                Assert.Fail();
            }
            catch (Exception)
            {
            }

            try
            {
                queue.CopyTo(null, array.Length);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void ClearTest(int[] array)
        {
            var queue = new Queue<int>(array);
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 14)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 }, 65)]
        [TestCase(new[] { 1 }, 4)]
        public void EnqueueTest(int[] array, int item)
        {
            var queue = new Queue<int>(array);
            queue.Enqueue(item);
            Assert.AreEqual(array.Length + 1, queue.Count);
            Assert.IsTrue(queue.Contains(item));
            Assert.IsTrue(queue.Dequeue() == array[0]);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 14)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 }, 65)]
        [TestCase(new[] { 1 }, 4)]
        public void DequeueTest(int[] array, int item)
        {
            var queue = new Queue<int>(array);
            var element = queue.Dequeue();
            Assert.AreEqual(array[0], element);
            Assert.AreEqual(array.Length - 1, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 3, 0)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 }, 4, -1)]
        [TestCase(new[] { 1 }, 1, -1)]
        public void ContainsTest(int[] array, int item1, int item2)
        {
            var queue = new Queue<int>(array);
            Assert.IsTrue(queue.Contains(item1));
            Assert.IsFalse(queue.Contains(item2));
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void ToArrayTest(int[] array)
        {
            var queue = new Queue<int>(array);
            Assert.IsTrue(array.SequenceEqual(queue.ToArray()));
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 7, 9 })]
        [TestCase(new[] { 1 })]
        public void TrimExcessTest(int[] array)
        {
            var queue = new Queue<int>(array);
            Assert.IsTrue(array.SequenceEqual(queue.ToArray()));
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.TrimExcess();
            Assert.AreEqual(array.Length, queue.Count);

            int i = 3;
            foreach (var element in queue)
            {
                Assert.AreEqual(i < array.Length ? array[i++] : 1, element);
            }
        }

        [Test]
        public void ForEachTest()
        {
            var queue = new Queue<int>(Enumerable.Range(1, 1000));
            int i = 1;
            foreach (var element in queue)
            {
                Assert.AreEqual(i++, element);
            }

            var iterator = queue.GetEnumerator();
            i = 1;
            while (iterator.MoveNext())
            {
                Assert.AreEqual(i++, iterator.Current);
            }
            iterator.Dispose();
        }

        [Test]
        public void ForEachExceptionTest()
        {
            var queue = new Queue<int>(Enumerable.Range(1, 10));
            var iterator = queue.GetEnumerator();
            queue.Enqueue(5);
            Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            iterator.Dispose();
        }
    }
}
