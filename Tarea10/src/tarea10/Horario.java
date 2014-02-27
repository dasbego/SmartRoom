//Esta clase representa el horario
package tarea10;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Horario {

    public Clase[][] clases;
    //Es una matriz de clases donde las columnas representan salones y las filas horas
    //Por ejemplo
    //
    //       Salon1  Salon2  Salon3
    // 8:30  Clase1  Clase2  Clase3
    // 10:00         Clase4
    // 11:30 Clase5  Clase6
    //
    
    //Esta es la lista de restricciones de que materias se pueden dar en cierta hora
    public List<Restriccion> restricciones;
    
    //Esta lista representa las posibles materias que se pueden dar
    //Por ejemplo el profe 1 puede dar la clase 2, 3 y 4 por decir algo
    //El chiste del problema va a ser colocar estas materias dentro de la matriz
    //Pero que entren todas las diferentes clases, sin importar el profe
    public List<Clase> posiblesClases;
    
    public Horario(int salones, int horas)
    {
        clases = new Clase[salones][horas];
        restricciones = new ArrayList<Restriccion>();
        posiblesClases = new ArrayList<Clase>();
    }
    
    //Metodo para checar si el horario es valido
    public boolean horarioValido()
    {
        //Un maestro no puede dar mas de una clase dada hora
        for(int hora = 0; hora < clases[0].length; hora++)
        {
            //Esta es una lista de los maestros que dan clase en la hora que se checa
            //Si se encuentra un maestro que ya esta en la lista quiere decir que esta dando más de una materia
            //Entonces el horario no es valido
            List<String> maestros = new ArrayList<String>();
            for(int salon = 0; salon < clases.length; salon++)
            {
                if(maestros.contains(clases[salon][hora].maestro))
                    return false;
                else 
                    maestros.add(clases[salon][hora].maestro);
            }   
        }
        //Luego hay que checar si la clase se puede dar a esa hora
        for(int salon = 0; salon < clases.length; salon++)
        {
            for(int hora = 0; hora < clases[salon].length; hora++)
            {
                Restriccion temp = new Restriccion();
                temp.hora = hora;
                temp.materia = clases[salon][hora].materia;
                if(restricciones.contains(temp))
                    return false;
            }
        }
        return true;
    }
    
    //Metodo para checar si esta completo el horario
    public boolean horarioCompleto()
    {
        //Lista de todas las materias que se deben impartir
        List<String> todasMaterias = new ArrayList<String>();
        for(int i = 0; i < posiblesClases.size(); i++)
        {
            if(!todasMaterias.contains(posiblesClases.get(i).materia))
                todasMaterias.add(posiblesClases.get(i).materia);
        }
        Collections.sort(todasMaterias);
        //Lista de todas las materias que se imparten
        List<String> materiasImpartidas =  new ArrayList<String>();
        for(int salon = 0; salon < clases.length; salon++)
        {
            for(int hora = 0; hora < clases[salon].length; hora++)
            {
                if(!materiasImpartidas.contains(clases[salon][hora].materia))
                    materiasImpartidas.add(clases[salon][hora].materia);
            }    
        }
        Collections.sort(materiasImpartidas);
        //El horario esta completo si es valido y se imparten todas las materias
        return horarioValido() && todasMaterias.equals(materiasImpartidas);
    }
}
