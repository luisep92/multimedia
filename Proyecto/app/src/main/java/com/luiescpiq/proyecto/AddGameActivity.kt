package com.luiescpiq.proyecto

import android.app.Activity
import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.inputmethod.InputMethodManager
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.firestore.FirebaseFirestore
import com.luiescpiq.proyecto.databinding.ActivityAddGameBinding
import com.luiescpiq.proyecto.databinding.ActivityMainBinding

// Luis Escolano Piquer

class AddGameActivity : AppCompatActivity() {
    private lateinit var binding: ActivityAddGameBinding
    private lateinit var db: Database

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAddGameBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = Database.instance

        setListeners()
        setFields()
    }

    private fun setFields(){
        // Cuando actualizamos rellenamos los campos, si es para añdir un nuevo juego nada.
        if (!intent.hasExtra(MainActivity.EXTRA_ID))
            return
        binding.txtDescription.setText(intent.getStringExtra(MainActivity.EXTRA_DESCRIPTION))
        binding.txtName.setText(intent.getStringExtra(MainActivity.EXTRA_NAME))
        binding.txtGenre.setText(intent.getStringExtra(MainActivity.EXTRA_GENRE))
        binding.ratingBar.rating = intent.getFloatExtra(MainActivity.EXTRA_RATING, 0.0f)
        binding.txtImg.setText(intent.getStringExtra(MainActivity.EXTRA_IMAGE))
        binding.btnInsert.text = "Actualizar"
    }

    private fun setListeners(){
        // Boton añadir/actualizar
        binding.btnInsert.setOnClickListener {
            if (!checkFields()){
                Snackbar.make(binding.root, "Faltan campos por rellenar", Snackbar.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            val id = if(intent.hasExtra(MainActivity.EXTRA_ID)) intent.getStringExtra(MainActivity.EXTRA_ID).toString() else ""
            db.insertGame(binding.txtName.text.toString(),
                                binding.txtGenre.text.toString(),
                                binding.txtDescription.text.toString(),
                                binding.txtImg.text.toString(),
                                binding.ratingBar.rating,
                                id
            )
            setResult(Activity.RESULT_OK)
            finish()
        }
        // Boton cancelar
        binding.btnCancel.setOnClickListener {
            setResult(Activity.RESULT_CANCELED)
            finish()
        }
        // Cuando ponemos valor al rating bar escondemos el teclado
        binding.ratingBar.setOnRatingBarChangeListener { _, rating, _ ->
            val inputMethodManager = getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
            inputMethodManager.hideSoftInputFromWindow(this.currentFocus?.windowToken, 0)
        }
    }

    // Comprobar campos vacíos
    private fun checkFields(): Boolean {
        return !(binding.ratingBar.rating == 0.0f ||
                binding.txtDescription.text.isBlank() ||
                binding.txtGenre.text.isBlank() ||
                binding.txtImg.text.isBlank() ||
                binding.txtName.text.isBlank())
    }
}