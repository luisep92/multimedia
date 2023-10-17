package com.example.mycolorsmyviews

import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.example.mycolorsmyviews.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // Estas dos líneas sustituyen a setContentView()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setListeners()
    }


    private fun colorearElemento(view: View) {
        when (view.id) {
            // Ponemos un color según el elemento correspondiente
            R.id.box_1_text -> view.setBackgroundColor(Color.DKGRAY)
            R.id.box_2_text -> view.setBackgroundColor(Color.GRAY)
            R.id.box_3_text -> view.setBackgroundColor(Color.BLUE)
            R.id.box_4_text -> view.setBackgroundColor(Color.MAGENTA)
            R.id.box_5_text -> view.setBackgroundColor(Color.BLUE)
            R.id.red_button -> binding.box3Text.setBackgroundColor(getColor(R.color.my_red))
            R.id.yellow_button -> binding.box4Text.setBackgroundColor(getColor(R.color.my_yellow))
            R.id.green_button -> binding.box5Text.setBackgroundColor(getColor(R.color.my_green))
            else -> view.setBackgroundColor(Color.LTGRAY)
        }
    }

    private fun setListeners()
    {
        binding.box1Text.setOnClickListener { colorearElemento(it) }
        binding.box2Text.setOnClickListener { colorearElemento(it) }
        binding.box3Text.setOnClickListener { colorearElemento(it) }
        binding.box4Text.setOnClickListener { colorearElemento(it) }
        binding.box5Text.setOnClickListener { colorearElemento(it) }
        binding.redButton.setOnClickListener { colorearElemento(it) }
        binding.yellowButton.setOnClickListener { colorearElemento(it) }
        binding.greenButton.setOnClickListener { colorearElemento(it) }
        binding.root.setOnClickListener { colorearElemento(it) }
    }
}