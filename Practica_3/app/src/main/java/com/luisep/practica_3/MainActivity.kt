package com.luisep.practica_3

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.google.android.material.tabs.TabLayoutMediator
import com.luisep.practica_3.databinding.ActivityMainBinding

// Luis Escolano Piquer

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    companion object{
        const val TAG_APP = "practica_3"
        const val EXTRA_ALUMNO = "myAlumno"
        const val EXTRA_DIA = "myDia"
        const val EXTRA_MES = "myMes"
        const val EXTRA_ANYO = "myAnyo"
        const val EXTRA_MODALIDAD = "myModalidad"
        const val EXTRA_CICLO = "myCiclo"
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val viewPager2 = binding.viewPager2
        val adapter = ViewpagerMainAdapter(supportFragmentManager, lifecycle)
        adapter.addFragment(MainFragment(), "Principal")
        adapter.addFragment(HistoryFragment(), "Histórico")
        viewPager2.adapter = adapter
        TabLayoutMediator(binding.tabLayout, viewPager2){tab, position ->
            tab.text = adapter.getPageTitle(position)}.attach()
    }
}
