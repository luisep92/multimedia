package com.luisep.practica_3

import android.app.Activity
import android.app.DatePickerDialog
import android.content.Intent
import android.os.Bundle
import android.widget.RadioGroup
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.luisep.practica_3.databinding.ActivityAddStudentBinding
import java.util.Calendar


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
            if(hasBlankFields()){
                Toast.makeText(this, getString(R.string.faltan_datos), Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Creamos fecha
            val dmy = binding.textViewFecha.text.split("/")
            val date = MyDate(dmy[0].toInt(), dmy[1].toInt(), dmy[2].toInt())

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
        //GET FECHA DATEPICKER
        binding.textViewFecha.setOnClickListener{
            setDate()
        }
    }

    // Abre el datepicker y recoge la fecha seleccionada
    private fun setDate() {
        val cal = Calendar.getInstance()
        val dateSetListener = DatePickerDialog.OnDateSetListener { _, year, month, day ->
            cal.set(Calendar.YEAR, year)
            cal.set(Calendar.MONTH, month)
            cal.set(Calendar.DAY_OF_MONTH, day)
            binding.textViewFecha.text =
                "${cal.get(Calendar.DAY_OF_MONTH)}/${cal.get(Calendar.MONTH) + 1}/${cal.get(Calendar.YEAR)}"
        }
        DatePickerDialog(
            this,
            dateSetListener,
            cal.get(Calendar.YEAR),
            cal.get(Calendar.MONTH),
            cal.get(Calendar.DAY_OF_MONTH)
        ).show()
    }


    // Comprobar datos en blanco
    private fun hasBlankFields(): Boolean {
        if (isRadioGroupEmpty(binding.radioGroup1))
            return true
        if (isRadioGroupEmpty(binding.radioGroup2))
            return true
        if (binding.textViewFecha.equals(R.string.fecha_nac))
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