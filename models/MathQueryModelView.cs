using Dangl.Calculator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace WPFCalculator.models
{
    public class MathQueryModelView
    {
        public string MathQueryString { get; set; } = "";
        public string MathQueryAnswer {
            get 
            {
                return Calculate(MathQueryArray.ToList());
            } 
        }
        public IEnumerable<string> MathQueryArray 
        { 
            get { return MathQueryString.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)); } 
        }
        public string ActualAnswer
        {
            get 
            {
                try
                {
                    return Convert.ToDecimal(Calculator.Calculate(MathQueryString).Result).ToString() ?? "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static string Operator(List<string> inputList)
        {
            try
            {
                inputList.RemoveAll(x => x is "(" or ")");

                if (!inputList.Exists(x => x is "+" or "-" or "*" or "/"))
                {
                    throw new Exception();
                }

                while (inputList.Exists(x => x is "**"))
                {
                    for (var i = 0; i < inputList.Count; i++)
                    {
                        if (inputList[i] == "**")
                        {
                            var result = Convert.ToDecimal(Math.Pow(Convert.ToDouble(inputList[i - 1]), Convert.ToDouble(inputList[i + 1])));
                            inputList.RemoveRange(i - 1, 3);
                            inputList.Insert(i - 1, result.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    }
                }

                while (inputList.Exists(x => x is "*" or "/"))
                {
                    for (var i = 0; i < inputList.Count; i++)
                    {
                        if (inputList[i] == "*")
                        {
                            var result = Convert.ToDecimal(inputList[i - 1]) * Convert.ToDecimal(inputList[i + 1]);
                            inputList.RemoveRange(i - 1, 3);
                            inputList.Insert(i - 1, result.ToString(CultureInfo.InvariantCulture));
                            break;
                        }

                        if (inputList[i] == "/")
                        {
                            var result = Convert.ToDecimal(inputList[i - 1]) / Convert.ToDecimal(inputList[i + 1]);
                            inputList.RemoveRange(i - 1, 3);
                            inputList.Insert(i - 1, result.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    }
                }

                while (inputList.Exists(x => x is "+" or "-"))
                {
                    for (var i = 0; i < inputList.Count; i++)
                    {
                        if (inputList[i] == "+")
                        {
                            var result = Convert.ToDecimal(inputList[i - 1]) + Convert.ToDecimal(inputList[i + 1]);
                            inputList.RemoveRange(i - 1, 3);
                            inputList.Insert(i - 1, result.ToString(CultureInfo.InvariantCulture));
                            break;
                        }

                        if (inputList[i] == "-")
                        {
                            var result = Convert.ToDecimal(inputList[i - 1]) - Convert.ToDecimal(inputList[i + 1]);
                            inputList.RemoveRange(i - 1, 3);
                            inputList.Insert(i - 1, result.ToString(CultureInfo.InvariantCulture));
                            break;
                        }
                    }
                }

                return Convert.ToDouble(inputList[0]).ToString();
            }
            catch (Exception)
            {
                throw new Exception("NaN");
            }
        }

        private string Calculate(List<string> inputList)
        {
            try
            {
                // Start to calculate inside the brackets
                while (inputList.Contains("("))
                {
                    var index1 = inputList.LastIndexOf("(");

                    if (index1 >= 0)
                    {
                        var index2 = inputList.FindIndex(index1, x => x == ")");
                        var subInputList = inputList.GetRange(index1, index2 - index1 + 1);
                        var result = Operator(subInputList);
                        inputList.RemoveRange(index1, index2 - index1 + 1);
                        inputList.Insert(index1, result);

                        if (index1 > 0 && double.TryParse(inputList[index1 - 1], out _))
                        {
                            inputList.Insert(index1, "*");
                        }
                    }
                }

                return Operator(inputList);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
