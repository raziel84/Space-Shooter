using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    //Una clase static no puede instanciarse ni funcionar como un componente en la jerarquia de objetos de Unity
	
    //Vamos a utilizar a realizar un método de extensión sobre el método de abajo
    //Los métodos de extensión permiten añadir métodos a clases ya existentes
    //Para ello entre () debemos colocar this, luego el tipo al que queremos asignar este método
    //y luego la variable que queremos utilizar para llamar a este método
    //Al utilizar this delante del parametro C# no lo entiende como uno.
    public static Vector2 GetDimensionsInWordUnits(this Camera camera) {

        //Creamos dos variables para guardar el ancho y el alto en unidades del mundo 3D
        float width, height;

        /* Dentro de este comentario está el contenido de este método sin usar el método de extensión
        //Referenciamos a la camara principal. Debe tener el tag MainCamera
        Camera cam = Camera.main;
        //Calculamos la relacion de aspecto de la camara
        //Casteamos a float uno de los dos valores porque ambos devuelven enteros y nos interesa la division en float
        //El ratio nos indica que porcentaje de la altura ocupa el ancho
        float ratio = cam.pixelWidth / (float) cam.pixelHeight;

        //En las camaras ortograficas el Size indica la cantidad de unidades del mundo que se dibujan
        //desde el centro de la pantalla hasta uno de los extremos
        //Multiplicando el mismo * 2 obtenemos el alto de la pantalla
        height = cam.orthographicSize * 2;
        */

        float ratio = camera.pixelWidth / (float)camera.pixelHeight;
        height = camera.orthographicSize * 2;

        //Sabiendo el porcentaje (ratio) de la altura que ocupa el ancho
        //Para calcularlo multiplicamos la altura por el ratio
        width = height * ratio;

        return new Vector2(width, height);
    }

}
