package com.luisep.practica_3

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import com.luisep.practica_3.Utils.Companion.getMode
import com.luisep.practica_3.databinding.ActivityMainBinding
import java.io.IOException
import java.io.OutputStreamWriter

// Luis Escolano Piquer

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var studentData: Array<Int>
    private lateinit var studentName: String
    companion object{
        const val TAG_APP = "practica_2"
        const val EXTRA_ALUMNO = "myAlumno"
        const val EXTRA_DIA = "myDia"
        const val EXTRA_MES = "myMes"
        const val EXTRA_ANYO = "myAnyo"
        const val EXTRA_MODALIDAD = "myModalidad"
        const val EXTRA_CICLO = "myCiclo"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setListeners()
    }


    fun setListeners(){
        // BOTON LEER DATOS - Nos lleva a la segunda actividad
        binding.buttonLeerDatos.setOnClickListener {
            if(binding.editTextNombreAlu.text.isBlank()){
                Toast.makeText(this, getString(R.string.falta_nombre), Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            openSecondActivity()
        }
        //BOTON OBTENER EDAD Y GRUPO - Mostramos texto y activamos el siguiente boton
        binding.buttonObtenerEdadGrupo.setOnClickListener {
            val date = MyDate(studentData[0], studentData[1], studentData[2])
            binding.textViewDatos.text = Utils.getInformationString(date, studentData[3], studentData[4], true)
            binding.buttonGuardar.isEnabled = true
            Utils.hideKeyboard(this)
        }
        // BOTON GUARDAR - Guardamos el registro y limpiamos los campos
        binding.buttonGuardar.setOnClickListener {
            addRegistry()
            binding.editTextNombreAlu.text.clear()
            binding.textViewDatos.text = ""
            binding.textViewFechaNac.text = getString(R.string.fecha)
            binding.textViewModCiclo.text = getString(R.string.modalidad_ciclo)
            binding.buttonObtenerEdadGrupo.isEnabled = false
            binding.buttonGuardar.isEnabled = false
        }
        //BOTON VER HISTORICO
        binding.buttonHistorico.setOnClickListener {
            openHistoryActivity()
        }
    }

    // Abre la actividad para rellenar los datos del alumno
    fun openSecondActivity(){
        val myIntent = Intent(this, ActivityAddStudent::class.java).apply {
            putExtra(EXTRA_ALUMNO, binding.editTextNombreAlu.text.toString())
        }
        getResult.launch(myIntent)
    }

    // Abre la actividad con la lista de alumnos
    fun openHistoryActivity(){
        val intent = Intent(this, HistoryActivity::class.java)
        startActivity(intent)
    }

    // Abre una actividad y devuelve un resultado cuando hacemos finish
    private val getResult = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
            setData(it)
            setTexts()
            binding.buttonObtenerEdadGrupo.isEnabled = true
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
            Toast.makeText(this, "Cancelado", Toast.LENGTH_SHORT)
        }
    }

    // Guardamos los datos que vienen de la segunda actividad
    fun setData(it: androidx.activity.result.ActivityResult) {
        val day = it.data!!.getIntExtra(EXTRA_DIA, -1)
        val month = it.data!!.getIntExtra(EXTRA_MES, -1)
        val year = it.data!!.getIntExtra(EXTRA_ANYO, -1)
        val mod = it.data!!.getIntExtra(EXTRA_MODALIDAD, -1)
        val course = it.data!!.getIntExtra(EXTRA_CICLO, -1)
        studentData = arrayOf(day, month, year, mod, course)
        studentName = it.data!!.getStringExtra(EXTRA_ALUMNO).toString()
    }

    //Actualiza los textos correspondientes cuando vuelves de las segunda actividad
    fun setTexts(){
        val date = MyDate(studentData[0], studentData[1], studentData[2])
        binding.textViewFechaNac.text = getString(R.string.fecha_nac_date, date)
        binding.textViewModCiclo.text =
            getString(
                R.string.modalidad_ciclo_2,
                getMode(studentData[3]),
                Utils.getCiclo(studentData[4])
            )
    }


    // Escribe un string en "registry.txt"
    private fun writeInFile(data: String) {
        try {
            val salida = OutputStreamWriter(openFileOutput("registry.txt", MODE_APPEND))
            salida.write(data + '\n')
            salida.flush()
            salida.close()
            Toast.makeText(this, "Guardado correctamente", Toast.LENGTH_SHORT).show()
        } catch (e: IOException) {
            Toast.makeText(this, e.message, Toast.LENGTH_SHORT).show()
        }
    }


    //Añade en nuestro archivo el estudiante actual
    private fun addRegistry(){

        val str = "" + studentData[0] + ";" +
                        studentData[1] + ";" +
                        studentData[2] + ";" +
                        binding.editTextNombreAlu.text.toString() + ";" +
                        studentData[3] + ";" +
                        studentData[4]

        writeInFile(str)
    }
}
