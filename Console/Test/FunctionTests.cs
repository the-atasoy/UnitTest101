using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test
{
    public static class FunctionTests
    {
        public static void Function_ReturnsPikachuIfZero_ReturnsString()
        {
            try
            {
                //arrange
                int num = 0;
                Function function = new Function();

                //act
                string result = function.ReturnsPikachuIfZero(num);

                //assert
                if (result == "Pikachu") Console.WriteLine("PASSED: Function_ReturnsPikachuIfZero_ReturnsString");
                else Console.WriteLine("FAILED: Function_ReturnsPikachuIfZero_ReturnsString");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
