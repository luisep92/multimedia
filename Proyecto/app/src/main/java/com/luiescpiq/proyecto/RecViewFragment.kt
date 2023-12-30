package com.luiescpiq.proyecto

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.luiescpiq.proyecto.databinding.FragmentRecViewBinding

// Luis Escolano Piquer


class RecViewFragment : Fragment() {
    private lateinit var binding: FragmentRecViewBinding
    private lateinit var db: Database
    private lateinit var gameList: MutableList<MyGame>

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,savedInstanceState: Bundle?): View? {
        binding = FragmentRecViewBinding.inflate(inflater)
        db = Database.instance
        gameList = MainActivity.gameList
        return binding.root
    }

    override fun onResume() {
        super.onResume()
        setupRecyclerView(gameList)
    }

    // Rellenar recycler view.
    private fun setupRecyclerView(list: MutableList<MyGame>){
        val myAdapter = GameAdapter(list, requireContext())
        binding.recViewGames.setHasFixedSize(true)
        binding.recViewGames.layoutManager = LinearLayoutManager(requireContext())
        binding.recViewGames.adapter = myAdapter
        db.addItemsToList(gameList, myAdapter)
    }
}