using System;

namespace RxBaba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Obersvable World!");

            var myObservableObject = new MyRangeObservable(5, 10);

            myObservableObject.Subscribe(new MyRangeObserver());

            Console.ReadKey();
        }
    }

    public class MyRangeObserver : IObserver<int>
    {
        public void OnNext(int value)
        {
            //You could print the value in the sequence
            Console.WriteLine("Iterated Value: " + value);
        }

        public void OnError(Exception error)
        {
            //Handle exception while iterating 
        }
        public void OnCompleted()
        {
            //Notifiy interested parties
            Console.WriteLine("Iteration completed !");
        }

    }

    public class MyRangeObservable : IObservable<int>
    {
        int _start = 0;
        int _count = 0;

        public MyRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;
        }
        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                for (int i = _start; i < _count; i++)
                {
                    observer.OnNext(i);
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
                return new MyDisposable();
            }

            observer.OnCompleted();
            return new MyDisposable();

        }
    }
}