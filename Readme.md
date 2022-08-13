# Moogle

![](moogle.png)

Proyecto de programacion primer anno
Curso: 2022-2023
Autor: `Hivan Cannizares Diaz`
# algoritmo

## TF-IDF 
TF-IDF es una estadistica numerica cuyo objetivo es reflejar la importancia de un termino o grupo de terminos en un grupo de documentos.
Su formula se calcula:
Tf*Idf
(Term Frecuency & Inverse Term Frecuency)
Esta estadistica es la base del programa de busqueda !Moogle
El siguiente codigo es el metodo tf_idf que obtiene este valor:
(Aqui se excluyen la utilizacion de los metodos de operadores, ya que estos no vienen al caso)
```csharp
    public static double tf_idf(Dictionary<string, int> Query_docs_occur, string text, int words_in_text, int docs_in_corpus, string query)
    {
        double tf_idf = 0;// Primero se crea la variable que ha de ser devuelva

        foreach (string word in Query_docs_occur.Keys)// Aqui se entra en un loop por cada palabra de la query Las cuales son las keys de este diccionario, siendo los values la cantidad de documentos en las que estas aparecen (Mas adelante se explicara el metodo que crea este diccionario)
        {                                                 
            tf_idf += Moogle.tf(text, word) * Moogle.idf(docs_in_corpus, Query_docs_occur[word]);// El tf-idf de la query es la suma de el tf-idf de cada palabra
        }

        return tf_idf;
    }

```
### TF 
```c#

```
TF(Term Frecuency)
Es la frecuencia relativa de un termino t en un documento d
En el programa existe como un metodo complementario de el metodo tf_idf
Este metodo toma el texto y la palabra a la que realizar el tf
Despues devuelve la cantidad de palabras que aparecen el texto con el metodo RawCount dividido con la cantidad de palabras del texto dado que se obtienen convirtiendo el texto en una lista y utilizando el metodo Length
en.wikipedia.org/wiki/Tf-idf
### IDF
IDF(Iverse Document Frecuency)
Es una medida de cuanta informacion otorga una palabra o cuan comun es en todos los textos 
Es el logaritmo de la division entre el total de documentos y la cantidad de documentos en que aparece la palabra 
El Metodo IDF recibe la cantidad de veces que aparece el termino en el texto y el total de documentos y tras realizar los calculos retorna el idf 
en.wikipedia.org/wiki/Tf-idf