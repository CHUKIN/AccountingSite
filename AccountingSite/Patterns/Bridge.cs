using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Patterns
{
   
          

    interface IResult
    {
        string Execute();
    }

    class PositiveResult : IResult
    {
        

        public string Execute()
        {
            return "Успешно";
        }
    }

    class NegativeResult : IResult
    {
       

        public string Execute()
        {
           return "Нет данного количества";
        }
    }

    abstract class Send
    {
        protected IResult result;
        public IResult Result
        {
            set { result = value; }
        }
        public Send(IResult lang)
        {
            result = lang;
        }
        public virtual string DoWork()
        {
            return result.Execute();
        }

    }

    class SendResult : Send
    {
        public SendResult(IResult lang) : base(lang)
        {
        }

    }
}