package com.luiescpiq.proyecto

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import androidx.activity.result.contract.ActivityResultContracts
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.android.material.snackbar.Snackbar
import com.luiescpiq.proyecto.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var db: Database
    private lateinit var gameList: MutableList<MyGame>
    companion object{
        const val TAG_APP = "Proyecto_Android"
        const val EXTRA_NAME = "myName"
        const val EXTRA_GENRE = "myGenre"
        const val EXTRA_DESCRIPTION = "myDescription"
        const val EXTRA_IMAGE = "myImage"
        const val EXTRA_RATING = "myRating"
        const val EXTRA_ID = "myId"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = Database.instance
        gameList = ArrayList()
        setupRecyclerView(gameList)
    }

    // Abrir actividad de añadir juego.
    fun openAddActivity(){
        val myIntent = Intent(this, AddGameActivity::class.java).apply {
        }
        getResultAdd.launch(myIntent)
    }

    // Recoger datos de la actividad de añadir.
    private val getResultAdd = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
            Snackbar.make(binding.root, "Juego añadido correctamente", Snackbar.LENGTH_SHORT).show()
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
            Snackbar.make(binding.root, "Cancelado", Snackbar.LENGTH_SHORT).show()
        }
    }

    // Abrir actividad editar.
    fun openEditActivity(name: String, genre: String, description: String, image: String, score: Float, id: String){
        val myIntent = Intent(this, AddGameActivity::class.java).apply {
            putExtra(EXTRA_NAME, name)
            putExtra(EXTRA_GENRE, genre)
            putExtra(EXTRA_DESCRIPTION, description)
            putExtra(EXTRA_IMAGE, image)
            putExtra(EXTRA_RATING, score)
            putExtra(EXTRA_ID, id)
        }
        getResultEdit.launch(myIntent)
    }

    // Abrir actividad detalles.
    fun openDetailsActivity(name: String, genre: String, description: String, image: String, score: Float, id: String) {
        val myIntent = Intent(this, DetailsActivity::class.java).apply {
            putExtra(EXTRA_NAME, name)
            putExtra(EXTRA_GENRE, genre)
            putExtra(EXTRA_DESCRIPTION, description)
            putExtra(EXTRA_IMAGE, image)
            putExtra(EXTRA_RATING, score)
            putExtra(EXTRA_ID, id)
        }
        startActivity(myIntent)
    }

    // Recoge datos de actividad editar.
    private val getResultEdit = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
            Snackbar.make(binding.root, "Juego editado correctamente", Snackbar.LENGTH_SHORT).show()
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
            Snackbar.make(binding.root, "Cancelado", Snackbar.LENGTH_SHORT).show()
        }
    }

    // Rellenar recycler view.
    private fun setupRecyclerView(list: MutableList<MyGame>){
        val myAdapter = GameAdapter(list, this)
        binding.recViewGames.setHasFixedSize(true)
        binding.recViewGames.layoutManager = LinearLayoutManager(this)
        binding.recViewGames.adapter = myAdapter
        db.addItemsToList(gameList, myAdapter)
    }

    // Menu principal, opción añadir.
    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when (item.itemId) {
            R.id.op01 -> {
                openAddActivity()
                true
            }
            else -> super.onOptionsItemSelected(item)
        }
    }

    // Crear menu opciones.
    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        val inflate = menuInflater
        inflate.inflate(R.menu.main_menu, menu)
        return true
    }
}