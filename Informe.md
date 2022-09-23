# Moogle

![](moogle.png)

Proyecto de programacion primer anno
Curso: 2022-2023
Autor: `Hivan Cannizares Diaz`

# Estructura Del Proyecto

El método principal del proyecto es ël método Query, a partir de este se realiza la busqueda utilizando métodos exteriores a este. En la mayor parte estos otros métodos se encuentran tanto en el archivo que contiene al método principal, así como en los otros archivos de extensión .cs que están en la carpeta MoogleEngine (Todos estos bajo el namespace MoogleEngine).

En la clase MatricesWork están los cimientos sobre los que se sostiene el algoritmo de búsqueda por lo que lo primero que debe ser explicado son los métodos que se encuentran ahí:

1.Dictionary<string, int>[] GetWordDictionarys(string[] files)

Este método es la base para construir lo que será la matriz de búsqueda para el proyecto. 

RECIBE: Un array de strings con los nombres de los archivos sobre los que se realizará la búsqueda.

DEVUELVE: Un array de Diccionarios con strings como Keys e int como Values. 

FUNCIONAMIENTO: El array Dictionarys creado al inicio es que ha de ser devuelto como resultado. Cada diccionario corresponde con un archivo de búsqueda, cada uno de estos diccionarios conteniendo cada palabra que aparece en el archivo como Key y su respectiva cantidad de veces que aparece en el texto. Esto se hace con un parde loops sencillos en los que no entraré en detalle porque no son realmente nada del otro mundo.

2.List<string> GetAllTheWords(Dictionary<string, int>[] Dictionarys)

Este es un sencillo método para extraer una lista con todas las palabras de todos los archivos a partir de el array de diccionarios del método anterior ciclando por todas las Keys(palabras) de cada diccionario y añadiéndolas a la lista.

3.int[,] GetMatrix(string[] files, Dictionary<string, int>[] Dictionarys, List<string> AllTheWords)
El array bidimensional que devuelve este método es una matrizen cuya dimensión 0 representa los archivos y la dimensión 1 representa cada palabra de cada texto. Esta matriz tiene la cantidad de veces que cada palabra aparece en cada texto. La elaboro ciclando por cada palabra de cada diccionario de el array de diccionarios creado anteriormente y dándole a cada valor la posición x(archivo), y(índice de la palabra en la lista AllTheWords creada anteriormente) en la matriz.

4.double[,] tf_idf(int[,] Matrix)
Este método toma la matriz creada en el método anterior y la convierte en una matriz de tf*idf. Este tiene dos submétodos auxiliares:

1.int[] GetTotalWords(int[,] Matrix)
Este método crea un array de int con la cantidad de palabras en total de cada texto con un doble for sobre la matriz. Estos datos serán necesarios para el cálculo del tf*idf.

2.int[] GetNumberOfDocsEachWordAppears(int[,] Matrix)
Como su nombre sugiere este método retorna un array con la cantidad de documentos en los que cada palabra aparece mediante otro doble for sobre la matriz.

Finalmente para calcular la matriz de tf*idf se realiza otro ciclo de doble for sobre la matriz que tenemos. El valor de cada elemento se calcula con la siguiente fórmula: (cant. de veces que la palabra aparece en el texto)/(Total de palabras en el texto)·log(cant. de docs./cant. de docs en los que aparece la palabra)(Fuente: Wikipedia en inglés)

Una vez que tenemos la matriz con los valores de cada palabra es necesario llevar la query recibida a vectores para que pueda ser fácilmente calculado el valor final de cada búsqueda en cada archivo y esto se hace con el método Sentence2Vec el cuál recibe la query y retorna un array con unos y ceros donde cada posición con un 1 es la posición de la palabra en la lista AllTheWords.

Teniendo la representación vectorial de la query y la matriz con los valores de búsqueda es necesario el método VecXMatrix que devuelve los valores de búsqueda para cada archivo de la oración en cuestión. Para calcular esto este método cicla por la primera dimensión de la matriz y luego por la segunda sumando los valores de la multiplicación de cada elemento de la fila por el elemnto de el vector (que siempre es uno), así obteniendo un array con el valor de la query en cada archivo.

Luegoestos resultados son usdos en el método:
SearchItem[] GetSearchItems(double[] SearchValues)
Que está en el archivo Moogle.cs en la clase Moogle y el método que lo contiene es el principal (Query).
Con el array de los valores de búsqueda este método crea una lista con todos los resultados diferentes de 0 de los SearchItems del SearchResult. El score saliendo de el método explicado anteriormente, el snippet es sacado del método GetSnip que extrae una pieza de texto de el tamaño deseado por el programador alrededor de la primrea ocurrencia de la palabra encontrada en el texto