//Holi
//Para que le entiendan empiecen por ver las clases de Clase y Restriccion que se ocupan para Horario
//Luego pasense a horario pa ver que hace
//Lo que logre fue crear el horario con posibles clases y restricciones
//El horario tiene metodos para checar si es valido y completo

//Falta
//Funcion para asignar una posible clase a un lugar en la matriz del horario
//Crear la funcion backtracking
//Implementar la heuristica

//De Bego:
/*segun yo, haria falta una varible (una lista) en clase Horario en la cual se puedan
 * tener un registro de las clases disponibles. Por ejemplo:
 * Materia   Profe Hora
 * Sistemas  Julio  0
 * Sistemas  Julio  1
 * Sistemas  Bego   0
 * Sistemas  Bego   1
 * 
 * La funcion de selectClass tendria que evaluar cuantas clases se quitarian de la lista de disponibles
 * y se elejiria la que menos restringe o quita menos disponibles de la lista.
 * si se elije por ejemplo
 * Sistemas Julio 0  -> Se quitarían las de Julio que puede dar en la hora 0.
 *                      Entonces por ejemplo si existia otro registro como Algoritmos Julio 0.
 *                      también se quitaría ya que esa ya no se puede dar por el salon ocupado.
 * 
 * Pense en acotar el numero de salones, ya que teniendo 4 hay muchas posibles formas de
 * acomodar las clases y entonces casi nunca haria backtracking. Esto porque tenemos muy pocas
 * clases para el ejemplo.
 * 
 * Entonces lo que se tendria que hacer:
 * -> Agregar un List<Clase> disponibles en Horario.java
 * -> En el Main generar la lista de clases disponibles de la clase horario de
 *      acuerdo con las Listas que ya se tienen.
 * -> La funcion backtracking que tiene que ser de alguna forma recursiva ya que
 *      necesita regresar a estados anteriores si es que choca con una restriccion.
 * -> Implementar la funcion selectClass que evalue que impacto tendria el seleccionar
 *      una opcion sobre la lista de disponibles. SE ELIJE LA QUE MENOS QUITE CLASES
 *      DISPONIBLES, y si hay empate hacer random.
 * 
 */

//Es bastante y esta medio latoso pero creo que si lo terminamos, el asunto es que hay que hacer un reporte
//El reporte pide resultados que ps hay que terminar esto para probarlo y planteamiento del problema
//Para el planteamiento basicamente hay que copiar los comentarios del codigo, ahi estan medio explicados


package tarea10;

import java.util.ArrayList;
import java.util.List;

public class main {

    public static void main(String[] args) {
        
        //Horario, cambie a que solo tuviera un salon porque si no las maneras de acomodarlos son muchas.
        //ya que para este ejemplo son pocas materias.
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
        
        
        //Mostrar horario.
        
    }
    
    
    
    
    //funcion backtracking
    public static Horario arrange(Horario horario){
        
        if(horario.horarioCompleto()){
            if(horario.horarioValido()){
                return horario;
            }
        }
      //  Clase nClass = selectClass(horario.posiblesClases, horario);
        
        return horario;
        
    }
    
    //funcion para seleccionar siguiente: Heuristica de la variable menos restringida
    /*
    public static void insertClasses(List<Clase> clases, Horario horario){
        
        List<Clase> disponiblesXhora = new ArrayList<Clase>();
        
        for (int i = 0; i < horario.horashorario; i++) {
            disponiblesXhora = horario.getClassesByHour(i);
            List <String> diffNames = getDifProfNamesFromList(disponiblesXhora);
            
            for(int j =0; j<horario.salonesTotal; j++)
            {
                String profNameTmp = diffNames.get(0);                
                int tmpCount = countProfList(disponiblesXhora, profNameTmp);
                
                
                for (int k = 0; k < diffNames.size(); k++) {
                    if(tmpCount > countProfList(disponiblesXhora, diffNames.get(k))){
                       tmpCount = countProfList(disponiblesXhora, diffNames.get(k));
                       String materia = disponiblesXhora.get(0).materia;
                       profNameTmp = diffNames.get(k);
                    }
                    
                }
            }
            
        }
        
        
        
        return null;
    }
    */
    public static List<String> getDifProfNamesFromList(List<Clase> clases)
    {
        List<String> names = new ArrayList<String>();        
        int size  = clases.size();
        
        String nameTmp = clases.get(0).maestro;
        names.add(nameTmp);
        
        for (int i = 1; i < size; i++) {            
            if(names.contains(clases.get(i).maestro))
                continue;
            else
                names.add(clases.get(i).maestro);
            
        }
        return names;
    }
    
    
    
    public static List<Clase> removeDuplicateProf(List<Clase> clases, String nameProf, String className)
    {
        List<Clase> resultList = new ArrayList<Clase>();
        for(Clase clase : clases)
        {
            if(clase.maestro.equals(nameProf) || clase.materia.equals(className))
                resultList.add(clase);
        }
        return resultList;
    }
    
    //Cuantas materias da un prof especifico en la lista de materias dada
    public static int countProfList(List<Clase> clases, String name)
    {
        int times=0;
        for(Clase clase : clases)
        {
            if( clase.maestro.equals(name))
                times++;
        }
        
        return times;
    }
    
}
