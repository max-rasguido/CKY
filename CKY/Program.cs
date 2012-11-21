using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKY
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Output = false; //Variable de salida del programa//
            List<Production> Grammar = new List<Production>(); //Lista de producciones de la gramatica//
            //Adicion de producciones a la gramatica//
            Grammar.Add(new Production("S","AA"));
            Grammar.Add(new Production("S","0"));
            Grammar.Add(new Production("A","SS"));
            Grammar.Add(new Production("A","1"));
            //Fin de adicion de producciones a la gramatica//

            string Word = "00001";//Palabra que se debe comprobar//
            List<Production>[,] CKYMat = new List<Production>[Word.Length,Word.Length];//Matriz de resultados//
            
            //Inicializacion de la matriz de resultados//
            for (int a = 0; a < Word.Length; a++)
            {
                for (int b = 0; b < Word.Length; b++)
                {
                    CKYMat[a, b] = new List<Production>();
                }
            }
            //Fin de inicializacion de la matriz//

            //Llenado de la primera fila de la matriz//
            for (int h = 0; h < Word.Length; h++)
            {
                foreach (Production prod in Grammar)
                {
                    if (Word[h].ToString() == prod.Produccion)
                    {
                        CKYMat[h, h].Add(prod);
                    }
                }
            }
            //Fin del llenado//

            int i1 = 0;//Variable estatica representando a la primera columna//
            int i = 0;//Variable que recorre la matriz//
            int j = 1;//Variable que recorre la matriz//
            int k = 0;//Variable que toma el valor de k//
            int k1 = 0;//Variable que toma el valor de k+1//
            List<string> Productos = new List<string>();//Lista de producciones a ser buscadas dentro de la gramatica//
            List<Production> Swaplist1 = new List<Production>();//Lista en la que se almacenan las producciones de una posicion de la matriz//
            List<Production> Swaplist2 = new List<Production>();//Lista en la que se almacenan las producciones de una posicion de la matriz//
            int column = Word.Length - 1;//Variable que indica la cantidad de veces que se debe trabajar en una columna//
            for(int je=1;je<Word.Length;je++)//Loop sobre el numero de fila en el que se deben encontrar productos//
            {
                j = je;
                i = i1;
                for (int v=0; v<column; v++)//Loop sobre el numero de columnas que se deben recorrer en la fila actual//
                {
                    for (int k2 = i; k2 < j; k2++)//Loop para encontrar k y tomar los generadores en las posiciones [i,k] y [k,j]
                    {
                        k = k2;
                        k1 = k2 + 1;
                        Swaplist1 = CKYMat[i, k];
                        Swaplist2 = CKYMat[k1, j];
                        if (Swaplist1.Count == 0 || Swaplist2.Count == 0)
                            break;
                        else
                        {
                            foreach (Production p1 in Swaplist1)
                            {
                                foreach (Production p2 in Swaplist2)
                                    Productos.Add(p1.Generador + p2.Generador);  
                            }
                        }
                    }
                    foreach (string str in Productos)//Loop para comprobar si las alguna Produccion en la gramatica genera los resultados encontrados por la union de los puntos encontrados por k//
                    {
                        foreach (Production prod in Grammar)
                        {
                            if (str == prod.Produccion)
                            {
                                CKYMat[i, j].Add(prod);//Si la produccion existe en la gramatica se agrega la regla que la produce a la matriz en esa posicion//
                            }
                        }
                    }
                    i++;
                    j++;
                }
                Productos.Clear();//Vaciado de la lista de producciones encontradas//
                column--;//Reduccion del numero de columnas a analizar (esto genera la escalera en la matriz)//
            }

            foreach (Production prod in Grammar)
            {
                if (prod.Generador == "S"&&CKYMat[0, Word.Length - 1].Contains(prod))//Se comprueba si existe un generador S en la punta de la escalera para ver si hay que devolver true//
                {
                    Output = true;
                }
            }
            Console.WriteLine(Output);//Escribe true si la palabra es generada por la gramatica//
            Console.Read();//Mantiene la consola abierta//
        }
    }
}
