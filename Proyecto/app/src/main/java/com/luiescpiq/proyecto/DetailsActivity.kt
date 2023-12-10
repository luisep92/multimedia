package com.luiescpiq.proyecto

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.luiescpiq.proyecto.databinding.ActivityAddGameBinding
import com.luiescpiq.proyecto.databinding.ActivityDetailsBinding
import com.squareup.picasso.Picasso

class DetailsActivity : AppCompatActivity() {
    private lateinit var binding: ActivityDetailsBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityDetailsBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setData()
    }

    // Rellenamos los datos de la actividad.
    private fun setData() {
        binding.txtDetailsName.setText(intent.getStringExtra(MainActivity.EXTRA_NAME))
        binding.txtDetailsGenre.setText(intent.getStringExtra(MainActivity.EXTRA_GENRE))
        binding.txtDetailsDescription.setText(intent.getStringExtra(MainActivity.EXTRA_DESCRIPTION))
        binding.ratingBarDetails.rating = intent.getFloatExtra(MainActivity.EXTRA_RATING, 0.0f)
        val img = intent.getStringExtra(MainActivity.EXTRA_IMAGE)
        Picasso.get().load(img).into(binding.imgDetails)
    }
}