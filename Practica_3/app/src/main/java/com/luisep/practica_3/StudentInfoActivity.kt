package com.luisep.practica_3

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.luisep.practica_3.databinding.ActivityStudentInfoBinding

// Luis Escolano Piquer

// Actividad donde se muestran los datos de un estudiante
class StudentInfoActivity : AppCompatActivity() {
    private lateinit var binding: ActivityStudentInfoBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityStudentInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Recogemos los datos
        val name = intent.getStringExtra(MainActivity.EXTRA_ALUMNO)
        val anyo = intent.getStringExtra(MainActivity.EXTRA_ANYO)
        val mes = intent.getStringExtra(MainActivity.EXTRA_MES)
        val dia = intent.getStringExtra(MainActivity.EXTRA_DIA)
        val modo = intent.getStringExtra(MainActivity.EXTRA_MODALIDAD)
        val ciclo = intent.getStringExtra(MainActivity.EXTRA_CICLO)

        // Asignamos los datos
        binding.textViewInfoNombre.text = name
        binding.textViewInfoFecha.text = getString(R.string.dia_mes_anyo, dia, mes, anyo)
        binding.textViewInfoGrupo.text = Utils.getGroup(ciclo!!, modo!!)
        binding.textViewInfoAula.text = Utils.getClass(ciclo!!, modo!!)
        binding.textViewCurso.text = getString(R.string.ciclo_modo, ciclo, modo)
        binding.root.setBackgroundColor(Utils.getColorFromCourse(ciclo, this))

        // Boton volver
        binding.floatingActionButtonVolver.setOnClickListener{
            finish()
        }
    }
}
