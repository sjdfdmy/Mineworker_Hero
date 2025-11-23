using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public static class RandomMatrix
{
    public static int[,] GetARandomMatrix(int row, int col,int rednum,int bluenum,int purplenum,int hardstonenum,int lavastonenum)
    {
        int[,] matrix = new int[row, col];

        for(int i=0;i<hardstonenum; i++)
        {
            List<Vector2> canuse=new List<Vector2>();

            for(int j=0;j<matrix.Length; j++)
            {
                if(matrix[j / col, j % col] == 0)
                {
                    canuse.Add(new Vector2(j/col,j%col));
                }
            }
            int randindex = Random.Range(0, canuse.Count);
            matrix[(int)canuse[randindex].x, (int)canuse[randindex].y] = -1;
            
        }
        

        return matrix;
    }

    public static string MatrixToString(int[,] matrix)
    {
        string result = "";
        int cellWidth = 4;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            System.Text.StringBuilder line = new System.Text.StringBuilder();
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                // 用 PadLeft 保证对齐
                line.Append(matrix[i, j].ToString().PadLeft(cellWidth));
            }
            result += line.ToString() + "\n";
        }
        return result;
    }


    static bool HaveBrokenStruct(int[,] matrix, int x, int y, int testid)
    {
        if (x < 0 || x >= matrix.GetLength(0) || y < 0 || y >= matrix.GetLength(1))
        {
            return true;
        }
        int nowx = x;
        int nowy = y;
        List<Vector2> savequeue = new List<Vector2>();
        savequeue.Add(new Vector2(nowx, nowy));

        int getnum = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((i == 0 && j == 0) || nowx + i < 0 || nowx + i >= matrix.GetLength(0) || nowy + j < 0 || nowy + j >= matrix.GetLength(1))
                {
                    continue;
                }
                if (matrix[nowx + i, nowy + j] == testid && !savequeue.Contains(new Vector2(nowx + i, nowy + j)))
                {
                    savequeue.Add(new Vector2(nowx + i, nowy + j));
                    getnum++;
                }
            }
        }
    }
}
