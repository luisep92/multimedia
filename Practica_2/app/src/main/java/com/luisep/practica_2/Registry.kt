package com.luisep.practica_2

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import com.luisep.practica_2.databinding.ItemStudentListBinding

// Luis Escolano Piquer

// Data class del registro del estudiante
data class Registry(val day: Int, val month: Int, val year: Int, val name: String, val modality: Int, val course: Int)


class RegistryAdapter(registryList: MutableList<Registry>, context: Context): RecyclerView.Adapter<RegistryAdapter.RegistryViewHolder>() {
    var myRegistries: MutableList<Registry>
    var myContext: Context

    init {
        myRegistries = registryList
        myContext = context
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RegistryViewHolder {
        val view = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.item_student_list, parent, false)
        return RegistryViewHolder(view)
    }
    override fun onBindViewHolder(holder: RegistryViewHolder, position: Int) {
        val item = myRegistries.get(position)
        holder.bind(item, myContext)
    }

    // Numero de registros en la lista
    override fun getItemCount(): Int {
        return myRegistries.size
    }

    // Aqui se bindean cada registro con su layout
    class RegistryViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        private val binding = ItemStudentListBinding.bind(view)
        fun bind(registry: Registry, context: Context) {
            // Rellenamos los datos del layout y cambiamos su color segun el curso
            binding.txtDia.text = registry.day.toString()
            binding.txtCurso.text = Utils.getCiclo(registry.course)
            binding.txtNombre.text = registry.name
            binding.txtAnyo.text = registry.year.toString()
            binding.txtMes.text = MyDate.monthToString(registry.month)
            binding.txtModalidad.text = Utils.getMode(registry.modality)
            binding.linearLayoutMain.setBackgroundColor(getColor(registry.course, context))
            // Efecto en onclick - saca un toast
            itemView.setOnClickListener {
                val date = MyDate(registry.day, registry.month, registry.year)
                val str = Utils.getInformationString(date, registry.modality, registry.course, false)
                Toast.makeText(context, str, Toast.LENGTH_LONG).show()
            }

        }

        // Color segun el curso al que pertenece un alumno
        fun getColor(course: Int, con: Context): Int {
            val c: Int
            when (course) {
                0 -> c = R.color.red
                1 -> c = R.color.blue
                2 -> c = R.color.green
                else -> c = R.color.white
            }
            return ContextCompat.getColor(con, c)
        }
    }
}



