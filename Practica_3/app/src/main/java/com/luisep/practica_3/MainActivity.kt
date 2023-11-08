package com.luisep.practica_3

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.core.content.ContextCompat.startActivity
import com.luisep.practica_3.MainActivity.Companion.EXTRA_ALUMNO
import com.luisep.practica_3.MainActivity.Companion.EXTRA_ANYO
import com.luisep.practica_3.MainActivity.Companion.EXTRA_CICLO
import com.luisep.practica_3.MainActivity.Companion.EXTRA_DIA
import com.luisep.practica_3.MainActivity.Companion.EXTRA_MES
import com.luisep.practica_3.MainActivity.Companion.EXTRA_MODALIDAD
import com.luisep.practica_3.Utils.Companion.getMode
import com.luisep.practica_3.databinding.ActivityMainBinding
import java.io.IOException
import java.io.OutputStreamWriter

// Luis Escolano Piquer

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    companion object{
        const val TAG_APP = "practica_3"
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
    }
}
