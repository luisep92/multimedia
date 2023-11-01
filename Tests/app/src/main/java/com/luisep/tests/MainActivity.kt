package com.luisep.tests

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.activity.result.contract.ActivityResultContracts
import com.luisep.tests.databinding.ActivityMainBinding
import kotlinx.serialization.*
import kotlinx.serialization.json.*

@Serializable
data class Person (val name: String, val age: Int, val mail: String)
class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    companion object {
        const val TAG_APP = "myExplicitIntent2"
        const val EXTRA_TITULO = "myTitulo"
        const val EXTRA_AUTOR = "myAutor"
        // Para recoger el comentario y el rating de la segunda actividad
        const val EXTRA_COMENTARIO = "myComentario"
        const val EXTRA_RATING = "myRating"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        binding.btEnviar.setOnClickListener {
            lanzarSegundaActividad()
        }
    }
    private fun lanzarSegundaActividad() {
        // Se crea un objeto de tipo Intent
        val myIntent = Intent(this, MainActivity2::class.java).apply {
            // Se añade la información a pasar por clave-valor
            putExtra(EXTRA_TITULO, binding.editTitulo.text.toString())
            putExtra(EXTRA_AUTOR, binding.editAutor.text.toString())
        }
        getResult.launch(myIntent)
    }
    private val getResult = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
            val comentario = it.data?.getStringExtra(EXTRA_COMENTARIO)
            val rating = it.data?.getFloatExtra(EXTRA_RATING, 0.0F).toString()
            binding.txtResult.text = "Valoración del Usuario\nRating: " + rating +
                    "\nComentario:\n" + comentario
            binding.txtResult.visibility = View.VISIBLE
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
            binding.txtResult.text = "Se ha cancelado"
            binding.txtResult.visibility = View.VISIBLE
        }
    }


//    override fun onCreate(savedInstanceState: Bundle?) {
//        super.onCreate(savedInstanceState)
//        setContentView(R.layout.activity_main)
//
//        // Serialize
//        val p = Person("Constan", 19, "konstan@gmail.com")
//        val str = Json.encodeToString(p)
//
//        //Deserialize
//        val str2 = "{\"name\":\"Pepe\", \"age\":25,\"mail\":\"viyuela@gmail.com\"}"
//        val p2 = Json.decodeFromString<Person>(str2)
//    }
}

