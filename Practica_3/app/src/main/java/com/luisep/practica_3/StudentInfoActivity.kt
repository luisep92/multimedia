package com.luisep.practica_3

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.luisep.practica_3.databinding.ActivityMainBinding
import com.luisep.practica_3.databinding.ActivityStudentInfoBinding

class StudentInfoActivity : AppCompatActivity() {
    private lateinit var binding: ActivityStudentInfoBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityStudentInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val name = intent.getStringExtra(MainActivity.EXTRA_ALUMNO)
        val anyo = intent.getStringExtra(MainActivity.EXTRA_ANYO)
        val mes = intent.getStringExtra(MainActivity.EXTRA_MES)
        val dia = intent.getStringExtra(MainActivity.EXTRA_DIA)
        val modo = intent.getStringExtra(MainActivity.EXTRA_MODALIDAD)
        val ciclo = intent.getStringExtra(MainActivity.EXTRA_CICLO)

        binding.textViewInfoNombre.text = name
        binding.textViewInfoFecha.text = "$dia de $mes de $anyo"
        binding.textViewInfoGrupo.text = Utils.getGroup(ciclo!!, modo!!)
        binding.textViewInfoAula.text = Utils.getClass(ciclo!!, modo!!)
        binding.textViewCurso.text = "$ciclo ($modo)"
        binding.root.setBackgroundColor(Utils.getColorFromCourse(ciclo, this))
    }
}
