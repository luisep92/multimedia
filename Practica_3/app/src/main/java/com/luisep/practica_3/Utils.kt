package com.luisep.practica_3

import android.app.Activity
import android.content.Context
import android.view.View
import android.view.inputmethod.InputMethodManager
import androidx.core.content.ContextCompat

// Luis Escolano Piquer

class Utils {
    // Companion object porque necesito que las funciones sean estáticas
    companion object{
        // Informacion del alumno, tiene un booleano para añadir o no la edad para poder reusarlo
        fun getInformationString(date: MyDate, presencial: Int, group: Int, age: Boolean): String{
            var ret : String
            ret = if(age)
                "Edad: " + date.toAge() + "\n"
            else
                ""
            when(presencial to group){
                0 to 0 -> ret += "Grupo A\nAula 101"
                1 to 0 -> ret += "Grupo B\nAula 102"
                0 to 1 -> ret += "Grupo C\nAula 201"
                1 to 1 -> ret += "Grupo D\nAula 202"
                0 to 2 -> ret += "Grupo E\nAula 301"
                1 to 2 -> ret += "Grupo F\nAula 302"
            }
            return ret
        }

        //
        fun getMode(mode: Int?): String {
            if(mode == 0)
                return "Presencial"
            if(mode == 1)
                return "Semipresencial"
            else
                return "?"
        }

        fun getCiclo(ciclo: Int?): String {
            if(ciclo == 0)
                return "ASIR"
            if(ciclo == 1)
                return "DAW"
            if(ciclo == 2)
                return "DAM"
            else
                return "?"
        }

        fun getGroup(group: String, modality: String): String{
            return when(modality to group){
                "Presencial"     to "ASIR" -> "Grupo A"
                "Semipresencial" to "ASIR" -> "Grupo B"
                "Presencial"     to "DAW" -> "Grupo C"
                "Semipresencial" to "DAW" -> "Grupo D"
                "Presencial"     to "DAM" -> "Grupo E"
                "Semipresencial" to "DAM" -> "Grupo F"
                else -> "ERROR"
            }
        }

        fun getClass(group: String, modality: String): String{
            return when(modality to group){
                "Presencial"     to "ASIR" -> "Aula 101"
                "Semipresencial" to "ASIR" -> "Aula 102"
                "Presencial"     to "DAW" -> "Aula 201"
                "Semipresencial" to "DAW" -> "Aula 202"
                "Presencial"     to "DAM" -> "Aula 301"
                "Semipresencial" to "DAM" -> "Aula 302"
                else -> "ERROR"
            }
        }

        // Color segun el curso al que pertenece un alumno
        fun getColorFromCourse(course: String, con: Context): Int {
            val c: Int
            when (course) {
                "ASIR" -> c = R.color.red
                "DAW" -> c = R.color.blue
                "DAM" -> c = R.color.green
                else -> c = R.color.white
            }
            return ContextCompat.getColor(con, c)
        }

        // Esconder el teclado
        fun View.hideKeyboard() {
            val inputMethodManager =
                context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
                inputMethodManager.hideSoftInputFromWindow(windowToken, 0)
        }
    }
}