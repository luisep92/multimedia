package com.luisep.practica_1

import java.util.Calendar

// Luis Escolano Piquer
// Esta clase está traducida desde java así que puede tener alguna cosa rara, aunque la he revisado

class MyDate(day: Int, month: Int, year: Int) {
    private var day = Int.MIN_VALUE
    private var month = Int.MIN_VALUE
    private var year = Int.MIN_VALUE

    init {
        setDay(day)
        setMonth(month)
        setYear(year)
    }

    fun getDay(): Int {
        return day
    }

    fun setDay(day: Int) {
        this.day = day
    }

    fun getMonth(): Int {
        return month
    }

    fun setMonth(month: Int) {
        this.month = month
    }

    fun getYear(): Int {
        return year
    }

    fun setYear(year: Int) {
        this.year = year
    }

    // Comprueba que la fecha exista y no sea futura
    fun isValid(): Boolean {
        if(!isValidDay(day))
            return false
        if(!isValidMonth(day, month))
            return false
        if(!isValidYear(day, month, year))
            return false
        if(isFuture())
            return false
        return true
    }

    // Fecha de hoy
    fun today(): MyDate {
        val today = Calendar.getInstance()
        val d = today.get(Calendar.DAY_OF_MONTH)
        val m = today.get(Calendar.MONTH) + 1
        val y = today.get(Calendar.YEAR)

        return MyDate(d, m , y)
    }

    // Comprueba si es una fecha futura
    fun isFuture(): Boolean{
        val today = today()

        if(this.year < today.year)
            return false
        if(this.year == today.year && this.month < today.month)
            return false
        if(this.year == today.year && this.month == today.month && this.day <= today.day)
            return false
        return true
    }

    // Da la edad desde una fecha hasta hoy
    fun toAge(): Int{
        val today: MyDate = today()
        var hasPassedBirthday = 0
        if (this.month > today.month)
            hasPassedBirthday += 1
        else if(this.month == today.month && this.day < today.day)
            hasPassedBirthday += 1
        return today.year - this.year - hasPassedBirthday
    }

    // Numero de dias de los meses
    private fun numDays(month: Int, year: Int = 1): Int {
        return when (month) {
            2 -> {
                if (isLeapYear(year))
                    29
                else
                    28
            }
            4, 6, 9, 11 -> 30
            else -> 31
        }
    }

    // Comprobacion año bisiesto
    private fun isLeapYear(year: Int): Boolean {
        if (year % 400 == 0)
            return true
        return year % 100 != 0 && year % 4 == 0
    }

    // Comprobar si un numero está entre un rango
    private fun isBetween(n: Int, min: Int, max: Int): Boolean {
        return n in (min + 1)..max
    }

    // Comprobar día válido
    private fun isValidDay(day: Int): Boolean {
        return isBetween(day, 0, 31)
    }

    // Comprobar mes válido
    private fun isValidMonth(month: Int): Boolean {
        return isBetween(month, 0, 12)
    }

    // Comprobar mes válido según el día
    private fun isValidMonth(day: Int, month: Int): Boolean {
        if (!isValidMonth(month))
            return false
        return day <= numDays(month, year)
    }

    // Comprobar año válido
    private fun isValidYear(day: Int, month: Int, year: Int): Boolean {
        return !(day == 29 && month == 2 && !isLeapYear(year))
    }

    override fun toString(): String {
        return getDay().toString() + "/" + getMonth() + "/" + getYear()
    }
}