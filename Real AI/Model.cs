using System;

namespace Real_AI
{
    public class Input : IDisposable
    {
        public int id;
        public string input;
        public int priority;

        public Input()
        {

        }

        public void Dispose()
        {
            
        }
    }

    public class Word : IDisposable
    {
        public int id;
        public string word;
        public int priority;

        public Word()
        {

        }

        public void Dispose()
        {

        }
    }

    public class Output : IDisposable
    {
        public int id;
        public string input;
        public string output;
        public int priority;

        public Output()
        {

        }

        public void Dispose()
        {

        }
    }

    public class Topic : IDisposable
    {
        public int id;
        public string input;
        public string topic;
        public int priority;

        public Topic()
        {

        }

        public void Dispose()
        {

        }
    }

    public class PreWord : IDisposable
    {
        public int id;
        public string word;
        public string preWord;
        public int priority;
        public int distance;

        public PreWord()
        {

        }

        public void Dispose()
        {

        }
    }

    public class ProWord : IDisposable
    {
        public int id;
        public string word;
        public string proWord;
        public int priority;
        public int distance;

        public ProWord()
        {

        }

        public void Dispose()
        {

        }
    }
}
