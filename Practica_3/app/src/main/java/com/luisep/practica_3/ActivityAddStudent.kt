package com.luisep.practica_3

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.RadioGroup
import android.widget.Toast
import com.luisep.practica_3.databinding.ActivityAddStudentBinding

// Luis Escolano Piquer

class ActivityAddStudent : AppCompatActivity() {
    private lateinit var binding: ActivityAddStudentBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAddStudentBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val name = intent.getStringExtra(MainActivity.EXTRA_ALUMNO)
        binding.textViewNombre1.text = getString(R.string.introduzca_los_datos_del_alumno, name)
        setListeners()
    }

    // Función para el botón, hacemos las comprobaciones y si no hay nada raro mostramos los datos
    private fun setListeners(){
        // BOTON ACEPTAR
        binding.buttonAceptar.setOnClickListener{
            // Escondemos el teclado
            Utils.hideKeyboard(this)
            // Si hay campos en blanco mostramos toast y salimos
            if(hasBlankFields()){
                Toast.makeText(this, getString(R.string.faltan_datos), Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Creamos fecha
            val date = MyDate(binding.editTextDia.text.toString().toInt(),
                              binding.editTextMes.text.toString().toInt(),
                              binding.editTextaAnyo.text.toString().toInt())

            // Comprobamos fecha y si no es valida mostramos toast  y salimos
            if (!date.isValid() || date.toAge() > 115) {
                Toast.makeText(this, "Fecha inválida", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            // Recogemos los datos del alumno actual y volvemos a la actividad anterior
            val data = getInfo(date)
            val intentResult: Intent = Intent().apply {
                putExtra(MainActivity.EXTRA_DIA, data[0])
                putExtra(MainActivity.EXTRA_MES, data[1])
                putExtra(MainActivity.EXTRA_ANYO, data[2])
                putExtra(MainActivity.EXTRA_MODALIDAD, data[3])
                putExtra(MainActivity.EXTRA_CICLO, data[4])
            }
            setResult(Activity.RESULT_OK, intentResult)
            finish()
        }
        //BOTON CANCELAR
        binding.buttonCancelar.setOnClickListener{
            setResult(Activity.RESULT_CANCELED)
            finish()
        }
    }

    // Comprobar datos en blanco
    private fun hasBlankFields(): Boolean {
        if (isRadioGroupEmpty(binding.radioGroup1))
            return true
        if (isRadioGroupEmpty(binding.radioGroup2))
            return true
        if (binding.editTextDia.text.isEmpty())
            return true
        if (binding.editTextMes.text.isEmpty())
            return true
        if (binding.editTextaAnyo.text.isEmpty())
            return true
        return false
    }


    // Array con los datos necesarios del alumno
    private fun getInfo(date: MyDate): Array<Int>{
        val presencial: Int = indexOfCheckedRadioButton(binding.radioGroup1)
        val group: Int = indexOfCheckedRadioButton(binding.radioGroup2)
        return arrayOf(date.getDay(), date.getMonth(), date.getYear(), presencial, group)
    }

    // Devuelve el indice del radio button activo en un radio group
    private fun indexOfCheckedRadioButton(radioGroup: RadioGroup): Int {
        val btnId = radioGroup.checkedRadioButtonId
        return radioGroup.indexOfChild(radioGroup.findViewById(btnId))
    }

    // Saber si no hay botones marcados en un radio group
    private fun isRadioGroupEmpty(rg: RadioGroup): Boolean {
        return indexOfCheckedRadioButton(rg) == -1
    }
}