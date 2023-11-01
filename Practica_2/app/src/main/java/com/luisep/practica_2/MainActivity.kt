package com.luisep.practica_2

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.activity.result.contract.ActivityResultContracts
import com.luisep.practica_2.databinding.ActivityAddStudentBinding
import com.luisep.practica_2.databinding.ActivityMainBinding


class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding

    companion object{
        const val TAG_APP = "practica_2"
        const val EXTRA_ALUMNO = "myAlumno"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setListeners()
    }


    fun setListeners(){
        binding.buttonLeerDatos.setOnClickListener {
            if(binding.editTextNombreAlu.text.isBlank())
                return@setOnClickListener
            openSecondActivity()
        }
    }

    fun openSecondActivity(){
        // Se crea un objeto de tipo Intent
        val myIntent = Intent(this, ActivityAddStudent::class.java).apply {
            // Se añade la información a pasar por clave-valor
            putExtra(EXTRA_ALUMNO, binding.editTextNombreAlu.text.toString())
        }
        getResult.launch(myIntent)
    }

    private val getResult = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
//            val comentario = it.data?.getStringExtra(EXTRA_COMENTARIO)
//            val rating = it.data?.getFloatExtra(EXTRA_RATING, 0.0F).toString()
//            binding.txtResult.text = "Valoración del Usuario\nRating: " + rating +
//                    "\nComentario:\n" + comentario
//            binding.txtResult.visibility = View.VISIBLE
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
//            binding.txtResult.text = "Se ha cancelado"
//            binding.txtResult.visibility = View.VISIBLE
        }
    }
}