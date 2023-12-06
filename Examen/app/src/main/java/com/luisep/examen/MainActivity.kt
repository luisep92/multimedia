package com.luisep.examen

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.snackbar.Snackbar
import com.luisep.examen.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setListeners()
    }

    fun setListeners(){
        binding.buttonIniciar.setOnClickListener {
            if (binding.editTextUsuario.text.toString().equals("david") &&
                binding.editTextPassword.text.toString().equals("1234")) {
                startActivity(Intent(this, SecondActivity::class.java))
            }
            else {
                Snackbar.make(binding.root,"Datos incorrectos", Snackbar.LENGTH_SHORT).show()
            }
        }
    }
}