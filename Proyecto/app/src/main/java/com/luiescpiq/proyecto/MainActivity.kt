package com.luiescpiq.proyecto

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.widget.ImageView
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.firebase.firestore.CollectionReference
import com.google.firebase.firestore.FirebaseFirestore
import com.luiescpiq.proyecto.databinding.ActivityMainBinding
import com.squareup.picasso.Picasso

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var db: Database
    private lateinit var gameList: MutableList<MyGame>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = Database.instance
        gameList = ArrayList()
        setupRecyclerView(gameList)
      //  ALj2YP6MWbPRXO5AslV4
        val game = hashMapOf(
            "name" to "OSU!",
            "category" to "Ritmo",
            "description" to "Clica los circulos al ritmo de la musica!",
            "image" to "https://i.imgur.com/yWwshRX.jpeg",
            "score" to 5.0f
        )
        db.getConnection().collection("Games").document("ALj2YP6MWbPRXO5AslV4").set(game)
            .addOnSuccessListener {
                Log.d("DOC_SET", "Clica los circulos al ritmo de la musica!")
            }
            .addOnFailureListener { e ->
                Log.w("DOC_SET", "Error inserting game.", e)
            }
    }

    private fun setupRecyclerView(list: MutableList<MyGame>){
        val myAdapter = GameAdapter(list, this)
        binding.recViewGames.setHasFixedSize(true)
        binding.recViewGames.layoutManager = LinearLayoutManager(this)
        binding.recViewGames.adapter = myAdapter
        db.addItemsToList(gameList, myAdapter)

    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when (item.itemId) {
            R.id.op01 -> {
                startActivity(Intent(this, AddGameActivity::class.java))
                true
            }
            else -> super.onOptionsItemSelected(item)
        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        val inflate = menuInflater
        inflate.inflate(R.menu.main_menu, menu)
        return true
    }
}