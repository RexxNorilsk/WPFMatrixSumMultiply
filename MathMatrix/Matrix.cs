using System;
using System.IO;
using System.Text;


public class Matrix<T>
{

    private T[,] data;

    public T this[int i, int j]
    {
        get {
            return Get(i, j);
        }
        set
        {
            Set(i, j, value);
        }
    }

    private int rows, cols;

    public Matrix(int _rows, int _cols)
    {
        rows = _rows;
        cols = _cols;
        data = new T[rows, cols];
    }

    public Matrix(int _rows, int _cols, Func<int, int, T> func)
    {
        rows = _rows;
        cols = _cols;
        data = new T[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                this[i, j] = func(i, j);
            }
        }
    }

    public void Set(int i, int j, T element) {
        data[i, j] = element;
    }

    public T Get(int i, int j)
    {
        return data[i, j];
    }

    public void SaveFromCSV(string pathName) {
        string result = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += this[i, j].ToString();
                if (j != cols - 1)result+=";";
            }
            result += "\n";
        }
        File.WriteAllText(pathName, result);
    }


    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b){
        if (a.cols != b.cols || a.rows != b.rows) throw new ArgumentException("Попытка суммировать матрицы разных размеров");
        else
        {
            Matrix<T> result = new Matrix<T>(a.rows, a.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result[i, j] = (dynamic)a[i, j] + (dynamic)b[i, j];
                }
            }
            return result;
        }
    }

    public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
    {
        if (a.cols != b.rows) throw new ArgumentException("Попытка умножения матриц некорректных размеров");
        else
        {
            int sums = a.cols;
            Matrix<T> result = new Matrix<T>(a.rows, b.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    dynamic sum = 0;
                    for (int k = 0;k < sums; k++) {
                        sum += (dynamic)a[i, k] * (dynamic)b[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
    }
}
