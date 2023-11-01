package com.luisep.tests

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import com.luisep.tests.databinding.ActivityMain2Binding

class MainActivity2 : AppCompatActivity() {
    private lateinit var binding: ActivityMain2Binding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMain2Binding.inflate(layoutInflater)
        setContentView(binding.root)
        // Se recuperan los datos y se asignan al TextView.
        val titulo = intent.getStringExtra(MainActivity.EXTRA_TITULO)
        val autor = intent.getStringExtra(MainActivity.EXTRA_AUTOR)
        binding.txtDatos.text = "Titulo: " + titulo + "\nAutor: " + autor
        binding.btAceptar.setOnClickListener {
            val intentResult: Intent = Intent().apply {
                // Se añade el valor del rating y del comentario.
                putExtra(MainActivity.EXTRA_RATING, binding.ratingBar.rating)
                putExtra(MainActivity.EXTRA_COMENTARIO, binding.editOpinion.text.toString())
            }
            Log.d(MainActivity.TAG_APP, "Se devuelve Aceptar")
            setResult(Activity.RESULT_OK, intentResult)
            // Finalizamos la actividad
            finish()
        }
        binding.btCancelar.setOnClickListener {
            setResult(Activity.RESULT_CANCELED)
            Log.d(MainActivity.TAG_APP, "Se devuelve Cancelar")
            // Finalizamos la actividad
            finish()
        }
    }

}