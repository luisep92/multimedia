package com.luisep.practica_3

import android.app.Activity
import android.content.Context
import android.view.View
import android.view.inputmethod.InputMethodManager

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


        // Esconder el teclado
        fun View.hideKeyboard() {
            val inputMethodManager =
                context.getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
                inputMethodManager.hideSoftInputFromWindow(windowToken, 0)
        }
    }
}