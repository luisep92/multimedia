package com.luiescpiq.proyecto

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.firestore.FirebaseFirestore
import com.luiescpiq.proyecto.databinding.ActivityAddGameBinding
import com.luiescpiq.proyecto.databinding.ActivityMainBinding

class AddGameActivity : AppCompatActivity() {
    private lateinit var binding: ActivityAddGameBinding
    private lateinit var db: Database

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAddGameBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = Database.instance

        setListeners()
    }
    private fun setListeners(){
        binding.btnInsert.setOnClickListener {
            if (!checkFields()){
                Snackbar.make(binding.root, "Faltan campos por rellenar", Snackbar.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            db.insertGame(binding.txtName.text.toString(),
                                binding.txtGenre.text.toString(),
                                binding.txtDescription.text.toString(),
                                binding.txtImg.text.toString(),
                                binding.ratingBar.rating)
            Snackbar.make(binding.root, "Juego insertado correctamente", Snackbar.LENGTH_SHORT).show()
            finish()
        }

        binding.btnCancel.setOnClickListener {
            finish()
        }
    }

    private fun checkFields(): Boolean {
        return !(binding.ratingBar.rating == 0.0f ||
                binding.txtDescription.text.isBlank() ||
                binding.txtGenre.text.isBlank() ||
                binding.txtImg.text.isBlank() ||
                binding.txtName.text.isBlank())
    }
}