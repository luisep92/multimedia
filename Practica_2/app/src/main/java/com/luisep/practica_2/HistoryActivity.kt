package com.luisep.practica_2

import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.luisep.practica_2.databinding.ActivityHistoryBinding
import java.io.BufferedReader
import java.io.IOException
import java.io.InputStreamReader
import java.util.Locale

// Luis Escolano Piquer


// Actividad donde se muestra la lista de alumnos
class HistoryActivity : AppCompatActivity() {
    private lateinit var binding: ActivityHistoryBinding
    private lateinit var myAdapter: RegistryAdapter
    private var order = true
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityHistoryBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setUpRecyclerView()
        // BOTON ORDENAR
        binding.floatingActionButton.setOnClickListener {
            if(order){
                myAdapter.myRegistries.sortBy { it.course }
            }
            else
                myAdapter.myRegistries.sortBy { it.name.lowercase() }
            myAdapter.notifyDataSetChanged()
            order = !order
        }
    }

    // Configurar recycler view
    private fun setUpRecyclerView() {
        binding.recyclerViewRegistries.setHasFixedSize(true)
        binding.recyclerViewRegistries.layoutManager = LinearLayoutManager(this)
        myAdapter = RegistryAdapter(readRegistry(), this)
        binding.recyclerViewRegistries.adapter = myAdapter
    }

    // Devuelve una lista de registros leyendo los datos de nuestro archivo
    private fun readRegistry(): ArrayList<Registry> {
        val ret = ArrayList<Registry>()
        val file = "registry.txt"
        if (fileList().contains(file)) {
            val input: InputStreamReader
            val br: BufferedReader
            try {
                input = InputStreamReader(openFileInput(file))
                br = BufferedReader(input)
                var linea = br.readLine()
                while (!linea.isNullOrEmpty()) {
                    val data: List<String> = linea.split(";")
                    ret.add(Registry(data[0].toInt(),
                        data[1].toInt(),
                        data[2].toInt(),
                        data[3],
                        data[4].toInt(),
                        data[5].toInt()))
                    linea = br.readLine()
                }

                input.close()
                br.close()
            } catch (e: IOException) {
                Toast.makeText(this, e.message, Toast.LENGTH_LONG).show()
            }
        } else {
            Toast.makeText(this, "Todavía no hay datos", Toast.LENGTH_LONG).show()
        }
        return ret
    }


}