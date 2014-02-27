//Holi
//Para que le entiendan empiecen por ver las clases de Clase y Restriccion que se ocupan para Horario
//Luego pasense a horario pa ver que hace
//Lo que logre fue crear el horario con posibles clases y restricciones
//El horario tiene metodos para checar si es valido y completo

//Falta
//Funcion para asignar una posible clase a un lugar en la matriz del horario
//Crear la funcion backtracking
//Implementar la heuristica

//Es bastante y esta medio latoso pero creo que si lo terminamos, el asunto es que hay que hacer un reporte
//El reporte pide resultados que ps hay que terminar esto para probarlo y planteamiento del problema
//Para el planteamiento basicamente hay que copiar los comentarios del codigo, ahi estan medio explicados


package tarea10;

import java.util.ArrayList;
import java.util.List;

public class main {

    public static void main(String[] args) {
        
        //Horario
        Horario horario = new Horario(4,4);
        //Posibles clases
        List<Clase> posiblesClases = new ArrayList<Clase>();
        //Restricciones
        List<Restriccion> restricciones = new ArrayList<Restriccion>();
        
        Clase tempClase = new Clase();
        Restriccion tempRestriccion = new Restriccion();
        
        //Agregar materias posibles
        //Primero agregamos los posibles profes y luego los posibles horarios
        
        //Sistemas inteligentes
        tempClase.materia = "Sistemas Inteligentes";
        tempClase.maestro = "Julio";
        posiblesClases.add(tempClase);
        tempClase.maestro = "Bego";
        posiblesClases.add(tempClase);
        tempRestriccion.materia = "Sistemas Inteligentes";
        tempRestriccion.hora = 0;
        restricciones.add(tempRestriccion);
        tempRestriccion.hora = 1;
        restricciones.add(tempRestriccion);
        
        //Calidad de Software
        tempClase.materia = "Calidad de Software";
        tempClase.maestro = "Bego";
        posiblesClases.add(tempClase);
        tempClase.maestro = "Pepe";
        posiblesClases.add(tempClase);
        tempRestriccion.materia = "Calidad de Software";
        tempRestriccion.hora = 1;
        restricciones.add(tempRestriccion);
        tempRestriccion.hora = 2;
        restricciones.add(tempRestriccion);
        
         //ITIL
        tempClase.materia = "ITIL";
        tempClase.maestro = "Pepe";
        posiblesClases.add(tempClase);
        tempClase.maestro = "Julio";
        posiblesClases.add(tempClase);
        tempRestriccion.materia = "ITIL";
        tempRestriccion.hora = 2;
        restricciones.add(tempRestriccion);
        tempRestriccion.hora = 3;
        restricciones.add(tempRestriccion);
        
        //Algoritmos
        tempClase.materia = "Algoritmos";
        tempClase.maestro = "Julio";
        posiblesClases.add(tempClase);
        tempClase.maestro = "Bego";
        posiblesClases.add(tempClase);
        tempRestriccion.materia = "Algoritmos";
        tempRestriccion.hora = 0;
        restricciones.add(tempRestriccion);
        tempRestriccion.hora = 3;
        restricciones.add(tempRestriccion);
        
        horario.posiblesClases = posiblesClases;
        horario.restricciones = restricciones;
        
    }
    
}
