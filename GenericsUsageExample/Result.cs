using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsUsageExample
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool Succedeed { get; set; }
        public ICollection<string> Errors { get; set; }

        public Result(T value, bool succeded, ICollection<string> errorList)
        {
            Value = value;
            Succedeed = succeded;
            Errors = errorList;
        }
        public Result(T value) : this(value, true, null) { }

        public Result(ICollection<string> errorList) : this(default(T), false, errorList) { }

        public static Result<T>  Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result<T> Failure(ICollection<string> errorList)
        {
            return new Result<T>(errorList);
        }

        public void AddError(string error)
        {
            if (Errors != null)
            {
                Errors.Add(error);
            }
            else
            {
                Errors = new List<string>{error};
            }
        }
    }
}
