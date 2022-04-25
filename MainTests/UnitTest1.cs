using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MainTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Different_Sum_Matrixes()
        {
            //Arrange
            Matrix<int> a, b, c;
            a = new Matrix<int>(5, 5);
            b = new Matrix<int>(2, 2);

            //Act
            c = a + b;

            //Assert
            Assert.AreEqual(a[0, 0], b[0,0], 1);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Different_Multiply_Matrixes()
        {
            //Arrange
            Matrix<int> a, b, c;
            a = new Matrix<int>(5, 5);
            b = new Matrix<int>(2, 2);

            //Act
            c = a * b;

            //Assert
            Assert.AreEqual(a[0, 0], b[0, 0], 1);
        }


        [TestMethod]
        public void Correct_Sum_Matrixes()
        {
            //Arrange
            Matrix<int> a, b, c, d;
            a = new Matrix<int>(2, 2);
            b = new Matrix<int>(2, 2);
            c = new Matrix<int>(2, 2);
            d = new Matrix<int>(2, 2);

            a[0, 1] = 5;
            b[0, 1] = 8;
            c[0, 1] = 13;



            //Act
            d = a + b;

            //Assert
            Assert.AreEqual(d[0, 1], c[0, 1], 0);
        }
        [TestMethod]
        public void Correct_Multiply_Matrixes()
        {
            //Arrange
            Matrix<int> a, b, c, d;
            a = new Matrix<int>(2, 2);
            b = new Matrix<int>(2, 2);
            c = new Matrix<int>(2, 2);
            d = new Matrix<int>(2, 2);

            for (int i=0;i<2;i++) {
                for (int j = 0; j < 2; j++)
                {
                    a[i, j] = 1;
                }
            }
            b[0, 0] = 2;
            b[0, 1] = 3;

            c[0, 0] = 2;
            c[0, 1] = 3;
            c[1, 0] = 2;
            c[1, 1] = 3;

            //Act
            d = a * b;

            //Assert
            Assert.AreEqual(d[0, 1], c[0, 1], 0);
        }

        [TestMethod]
        public void Generate_Matrix_From_Lambda()
        {
            //Arrange
            Matrix<int> a;

            //Act
            a = new Matrix<int>(2, 2, (i, j) => {
                return i * j;
            });

            //Assert
            Assert.AreEqual(a[1, 1], 1, 0);
        }
    }
}
