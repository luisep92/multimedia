package com.luiescpiq.proyecto

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.luiescpiq.proyecto.databinding.FragmentRecViewBinding
import com.luiescpiq.proyecto.databinding.FragmentTop5Binding

// Luis Escolano Piquer


class Top5Fragment : Fragment() {
    private lateinit var binding: FragmentTop5Binding
    private lateinit var db: Database
    private lateinit var gameList: MutableList<MyGame>
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        binding = FragmentTop5Binding.inflate(inflater)
        db = Database.instance
        gameList = MainActivity.gameList
        return binding.root
    }
    override fun onResume() {
        super.onResume()
        setupRecyclerView(gameList)
    }

    private fun setupRecyclerView(list: MutableList<MyGame>){
        val myAdapter = GameAdapter(list, requireContext())
        binding.recViewGames.setHasFixedSize(true)
        binding.recViewGames.layoutManager = LinearLayoutManager(requireContext())
        binding.recViewGames.adapter = myAdapter
        db.addBestItemsToList(gameList, myAdapter)
    }
}