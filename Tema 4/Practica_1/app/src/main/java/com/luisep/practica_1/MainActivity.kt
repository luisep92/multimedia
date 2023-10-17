package com.luisep.practica_1

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.MediaStore.Audio.Radio
import android.view.inputmethod.InputMethodManager
import android.widget.RadioGroup
import android.widget.Toast
import androidx.core.view.isEmpty
import com.luisep.practica_1.databinding.ActivityMainBinding

// Luis Escolano Piquer

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setListeners()
    }

    // Función para el botón, hacemos las comprobaciones y si no hay nada raro mostramos los datos
    fun setListeners(){
        binding.buttonObtenerDatos.setOnClickListener{
            // Escondemos el teclado
            hideKeyboard()
            /* Limpio el texto para que al probar diferentes usuarios, si ponemos uno valido y
               luego uno inválido, se quite el anterior */
            setTextToShow("", "")
            // Si hay campos en blanco mostramos toast y salimos
            if(hasBlankFields()){
                Toast.makeText(this, "Hay campos sin datos introducidos", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Creamos fecha
            val date = MyDate(binding.editTextDia.text.toString().toInt(),
                              binding.editTextMes.text.toString().toInt(),
                              binding.editTextaAnyo.text.toString().toInt())

            // Comprobamos fecha y si no es valida mostramos toast  y salimos
            if (!date.isValid()) {
                Toast.makeText(this, "Fecha inválida", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Llegados aquí significa que no hay fallos, mostramos los datos
            setTextToShow(getInformationString(date), binding.editTextNombre.text.toString())
        }
    }

    // Comprobar datos en blanco
    private fun hasBlankFields(): Boolean {
        if (isRadioGroupEmpty(binding.radioGroup1))
            return true
        if (isRadioGroupEmpty(binding.radioGroup2))
            return true
        if (binding.editTextNombre.text.isEmpty())
            return true
        if (binding.editTextDia.text.isEmpty())
            return true
        if (binding.editTextMes.text.isEmpty())
            return true
        if (binding.editTextaAnyo.text.isEmpty())
            return true
        return false
    }

    /* INFO GRUPOS POSIBLES
    Presencial ASIR -     A / 101
    Semipresencial ASIR - B / 102
    Presencial DAW -      C / 201
    Semipresencial DAW-   D / 202
    Presencial DAM        E / 301
    Semipresencial DAM    F / 302
    */

    // Texto con los datos que vamos a mostrar en el text view gris
    private fun getInformationString(date: MyDate): String{
        val presencial: Int = indexOfCheckedRadioButton(binding.radioGroup1)
        val group: Int = indexOfCheckedRadioButton(binding.radioGroup2)
        var ret: String = "Edad: " + date.toAge() + "\n \n"
        when(presencial to group){
            0 to 0 -> ret += "Grupo A\n \nAula 101"
            1 to 0 -> ret += "Grupo B\n \nAula 102"
            0 to 1 -> ret += "Grupo C\n \nAula 201"
            1 to 1 -> ret += "Grupo D\n \nAula 202"
            0 to 2 -> ret += "Grupo E\n \nAula 301"
            1 to 2 -> ret += "Grupo F\n \nAula 302"
        }
        return ret
    }

    // Devuelve el indice del radio button activo en un radio group
    private fun indexOfCheckedRadioButton(radioGroup: RadioGroup): Int {
        val btnId = radioGroup.checkedRadioButtonId
        return radioGroup.indexOfChild(radioGroup.findViewById(btnId))
    }

    // Texto de los datos a mostrar
    private fun setTextToShow(resultText: String, nameText: String){
        binding.textViewResultado.text = resultText
        binding.textViewMostrarNombre.text = nameText
    }

    // Esconder el teclado
    private fun hideKeyboard() {
        val imm = getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.hideSoftInputFromWindow(binding.buttonObtenerDatos.windowToken, 0)
    }

    // Saber si no hay botones marcados en un radio group
    private fun isRadioGroupEmpty(rg: RadioGroup): Boolean {
        return indexOfCheckedRadioButton(rg) == -1
    }
}