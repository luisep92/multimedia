package com.luiescpiq.apuntes

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.luiescpiq.apuntes.databinding.ActivityMainBinding
import com.luiescpiq.apuntes.databinding.ActivityRecViewBinding
import com.luiescpiq.apuntes.databinding.ItemDiscoListBinding

class RecViewActivity : AppCompatActivity() {
    private lateinit var binding: ActivityRecViewBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)


        binding = ActivityRecViewBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setUpRecyclerView()
    }

    private fun setUpRecyclerView() {
        // Esta opción a TRUE significa que el RV tendrá
        // hijos del mismo tamaño, optimiza su creación.
        binding.myRVDiscos.setHasFixedSize(true)
        // Se indica el contexto para RV en forma de lista.
        binding.myRVDiscos.layoutManager = LinearLayoutManager(this)
        // Se genera el adapter.
        val myAdapter = DiscoAdapter(getDiscos(), this)
        // Se asigna el adapter al RV.
        binding.myRVDiscos.adapter = myAdapter
    }

    private fun getDiscos(): MutableList<MyDisco> {
        val discos: MutableList<MyDisco> = arrayListOf()
        discos.add(MyDisco("Abbey Road", "The Beatles", R.mipmap.ic_launcher))
        discos.add(MyDisco("Born to Run", "Bruce Springsteen",
            R.mipmap.ic_launcher_round))
        discos.add(MyDisco("Exile on main st.", "The Rolling Stones",
            R.mipmap.ic_launcher_round))
        discos.add(MyDisco("Ten", "Pearl Jam", R.mipmap.ic_launcher))
        discos.add(MyDisco("Nocturnal", "Amaral", R.mipmap.ic_launcher_round))
        discos.add(MyDisco("Daiquiri Blues", "Quique González",
            R.mipmap.ic_launcher))
        discos.add(MyDisco("White Album", "The Beatles", R.mipmap.ic_launcher_round))
        discos.add(MyDisco("24 Nights", "Eric Clapton", R.mipmap.ic_launcher))
        discos.add(MyDisco("Shake your moneymaker", "The Black Crowes",
            R.mipmap.ic_launcher_round))
        return discos
    }
}

data class MyDisco (val titulo: String,
                    val autor: String,
                    val imagen: Int)

class DiscoViewHolder(view: View) : RecyclerView.ViewHolder(view) {
    // Se usa View Binding para localizar los elementos en la vista.
    // Evitamos de esa forma la utilización de findViewById
    private val binding = ItemDiscoListBinding.bind(view)
    fun bind(disco: MyDisco, context: Context) {
        binding.tvTituloDisco.text = disco.titulo
        binding.tvAutorDisco.text = disco.autor
        binding.ivDiscoImage.setImageResource(disco.imagen)
        // Definimos el código a ejecutar si se hace click en el item
        itemView.setOnClickListener {
            Toast.makeText(
                context,
                disco.titulo,
                Toast.LENGTH_SHORT
            ).show()
        }
    }
}

class DiscoAdapter (discosList: MutableList<MyDisco>, context: Context): RecyclerView.Adapter<DiscoAdapter.DiscoViewHolder>() {
    var myDiscos: MutableList<MyDisco>
    var myContext: Context
    init {
        myDiscos = discosList
        myContext = context
    }

    // Es el encargado de devolver el ViewHolder ya configurado
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int):
            DiscoViewHolder {
        val view = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.item_disco_list, parent, false)
        return DiscoViewHolder(view)
    }
    override fun onBindViewHolder(holder: DiscoViewHolder, position: Int) {
        val item = myDiscos.get(position)
        holder.bind(item, myContext)
    }
    override fun getItemCount(): Int {
        return myDiscos.size
    }
    class DiscoViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        // Aquí es necesario utilizar findViewById para localizar el elemento
        // de la vista que se pasa como parámetro.
        private val titulo = view.findViewById(R.id.tv_tituloDisco) as
                TextView
        private val autor = view.findViewById(R.id.tv_autorDisco) as TextView
        private val discoImg = view.findViewById(R.id.iv_discoImage) as
                ImageView
        fun bind(disco: MyDisco, context: Context) {
            Log.d("bind", disco.titulo.toString())
            titulo.text = disco.titulo
            autor.text = disco.autor
            discoImg.setImageResource(disco.imagen)
            // Definimos el código a ejecutar si se hace click en el item
            itemView.setOnClickListener {
                Toast.makeText(
                    context,
                    disco.titulo,
                    Toast.LENGTH_SHORT
                ).show()
            }
        }
    }
}

