package com.luisep.practica_3

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import com.luisep.practica_3.databinding.FragmentHistoryBinding
import com.luisep.practica_3.databinding.FragmentMainBinding
import java.io.BufferedReader
import java.io.IOException
import java.io.InputStreamReader

// Luis Escolano Piquer

class HistoryFragment: Fragment() {
    private lateinit var binding: FragmentHistoryBinding
    private lateinit var myAdapter: RegistryAdapter
    private var order = true

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        binding = FragmentHistoryBinding.inflate(inflater)

        // BOTON ORDENAR
        binding.floatingActionButton.setOnClickListener {
            if(order){
                // Ordenar por curso
                myAdapter.myRegistries.sortBy { it.course }
            }
            else
                // Ordenar por nombre
                myAdapter.myRegistries.sortBy { it.name.lowercase() }
            myAdapter.notifyDataSetChanged()
            order = !order
        }
        return binding.root
    }

    override fun onResume() {
        super.onResume()
        setUpRecyclerView()
    }

    // Configurar recycler view
    private fun setUpRecyclerView() {
        binding.recyclerViewRegistries.setHasFixedSize(true)
        binding.recyclerViewRegistries.layoutManager = LinearLayoutManager(requireContext())
        myAdapter = RegistryAdapter(readRegistry(), requireContext())
        binding.recyclerViewRegistries.adapter = myAdapter
    }

    // Devuelve una lista de registros leyendo los datos de nuestro archivo
    private fun readRegistry(): ArrayList<Registry> {
        val ret = ArrayList<Registry>()
        val file = "registry.txt"
        if (requireContext().fileList().contains(file)) {
            val input: InputStreamReader
            val br: BufferedReader
            try {
                input = InputStreamReader(requireContext().openFileInput(file))
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
                Toast.makeText(requireContext(), e.message, Toast.LENGTH_LONG).show()
            }
        } else {
            Toast.makeText(requireContext(), getString(R.string.no_hay_datos), Toast.LENGTH_LONG).show()
        }
        return ret
    }
}