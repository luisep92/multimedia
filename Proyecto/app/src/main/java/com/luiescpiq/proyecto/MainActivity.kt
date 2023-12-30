package com.luiescpiq.proyecto

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import androidx.activity.result.contract.ActivityResultContracts
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.google.android.material.snackbar.Snackbar
import com.google.android.material.tabs.TabLayoutMediator
import com.luiescpiq.proyecto.databinding.ActivityMainBinding

// Luis Escolano Piquer


class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding

    companion object{
        var gameList: MutableList<MyGame> = ArrayList()
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

        // Gestión viewpager
        val viewPager2 = binding.viewPager2
        val adapter = ViewPager2Adapter(supportFragmentManager, lifecycle)
        adapter.addFragment(RecViewFragment(), "Todos")
        adapter.addFragment(Top5Fragment(), "Los mejores")
        viewPager2.adapter = adapter
        TabLayoutMediator(binding.tabLayout, viewPager2){tab, position ->
            tab.text = adapter.getPageTitle(position)
        }.attach()
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

// ViewPager
class ViewPager2Adapter(
    fragmentManager: FragmentManager, lifecycle: Lifecycle) : FragmentStateAdapter(fragmentManager, lifecycle){
    private val mFragmentList = ArrayList<Fragment>()
    private val mFragmentTitleList = ArrayList<String>()
    override fun getItemCount(): Int {
        return mFragmentList.size
    }
    override fun createFragment(position: Int): Fragment {
        return mFragmentList[position]
    }
    fun addFragment(fragment: Fragment, title: String) {
        mFragmentList.add(fragment)
        mFragmentTitleList.add(title)
    }
    fun getPageTitle(position: Int): CharSequence? {
        return mFragmentTitleList[position]
    }
}