package com.luisep.practica_3

import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import com.luisep.practica_3.databinding.ItemStudentListBinding

// Luis Escolano Piquer

// Data class del registro del estudiante
data class Registry(val day: Int, val month: Int, val year: Int, val name: String, val modality: Int, val course: Int)

// Adapter de la clase Registry
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

    // Aqui de bindean cada registro con su layout
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
            // Efecto en onclick
            itemView.setOnClickListener {
                val intent = Intent(context, StudentInfoActivity::class.java).apply {
                    putExtra(MainActivity.EXTRA_ALUMNO, binding.txtNombre.text.toString())
                    putExtra(MainActivity.EXTRA_ANYO, binding.txtAnyo.text.toString())
                    putExtra(MainActivity.EXTRA_DIA, binding.txtDia.text.toString())
                    putExtra(MainActivity.EXTRA_MES, binding.txtMes.text.toString())
                    putExtra(MainActivity.EXTRA_CICLO, binding.txtCurso.text.toString())
                    putExtra(MainActivity.EXTRA_MODALIDAD, binding.txtModalidad.text.toString())
                }
                context.startActivity(intent)
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



