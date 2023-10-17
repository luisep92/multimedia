package com.example.myhelloworld

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.example.myhelloworld.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //setContentView(R.layout.activity_main)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        binding.buttonMain.setOnClickListener {mensajeSaludo()}
    }

    private fun mensajeSaludo() {
        val et_name: EditText
        et_name = findViewById(R.id.editText_name)
        if (et_name.text.toString().isEmpty()) {
            Toast.makeText(this, "I need your name",
                Toast.LENGTH_SHORT).show()
        } else {
            Toast.makeText(this, "Hi ${et_name.text}!!",
                Toast.LENGTH_SHORT).show()
        }
    }

}