package com.luisep.examen

import android.content.Intent
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.firestore.CollectionReference
import com.google.firebase.firestore.FirebaseFirestore
import com.luisep.examen.databinding.ActivitySecondBinding

class SecondActivity : AppCompatActivity() {
    private lateinit var binding: ActivitySecondBinding
    private lateinit var db: FirebaseFirestore

    companion object{
        var filter = "android"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySecondBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = FirebaseFirestore.getInstance()

        binding.buttonIntroducir.setOnClickListener {
            if(!checkFields())
                Snackbar.make(binding.root,"Hay campos vacíos", Snackbar.LENGTH_SHORT).show()
            else {
                val app = binding.editTextApp.text.toString()
                val tema = binding.editTextTema.text.toString()
                val horas = binding.editTextHoras.text.toString()
                val tipo: String
                if(binding.radioButton.isChecked)
                    tipo = "android"
                else
                    tipo = "unity"
                binding.textViewMostrar.setText(
                    "Aplicación: ${binding.editTextApp.text}\nTema ${binding.editTextTema.text}\nHoras Implementación: ${binding.editTextHoras.text}"
                )
                insertApp(app, horas, tema, tipo)
            }

            }
        binding.buttonActivity.setOnClickListener{
            if(binding.radioButton.isChecked)
                filter = "android"
            else
                filter = "unity"
            startActivity(Intent(this, RecViewActivity::class.java))
        }
    }

    fun checkFields(): Boolean{
        return !(binding.editTextApp.text.isBlank() ||
                binding.editTextTema.text.isBlank() ||
                binding.editTextHoras.text.isBlank() ||
                (!binding.radioButton.isChecked && !binding.radioButton2.isChecked))
    }

    fun insertApp(app: String, hours: String, tema: String, tipo: String){
        val appCollection: CollectionReference = db.collection("Apps")
        val app = hashMapOf(
            "aplicacion" to app,
            "hours" to hours,
            "tema" to tema,
            "tipo" to tipo
        )
        appCollection.document().set(app)
            // Respuesta si ha sido correcto.
            .addOnSuccessListener {
                Log.d("DOC_SET", "Documento añadido!")
            }
            // Respuesta si se produce un fallo.
            .addOnFailureListener { e ->
                Log.w("DOC_SET", "Error en la escritura", e)
            }
    }
}