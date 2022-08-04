# algoritmo
## TF-IDF 

### TF 
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