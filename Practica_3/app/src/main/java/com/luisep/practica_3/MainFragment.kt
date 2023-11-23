package com.luisep.practica_3

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.snackbar.Snackbar
import com.luisep.practica_3.Utils.Companion.hideKeyboard
import com.luisep.practica_3.databinding.ActivityMainBinding
import com.luisep.practica_3.databinding.FragmentMainBinding
import java.io.IOException
import java.io.OutputStreamWriter

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [MainFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class MainFragment : Fragment() {
    // TODO: Rename and change types of parameters
    private lateinit var binding: FragmentMainBinding
    private var param1: String? = null
    private var param2: String? = null
    private lateinit var studentData: Array<Int>
    private lateinit var studentName: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            param1 = it.getString(ARG_PARAM1)
            param2 = it.getString(ARG_PARAM2)
        }
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        binding = FragmentMainBinding.inflate(inflater)
        setListeners()
        return binding.root
    }

    companion object {
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            MainFragment().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
    fun setListeners(){
        // BOTON LEER DATOS - Nos lleva a la segunda actividad
        binding.buttonLeerDatos.setOnClickListener {
            if(binding.editTextNombreAlu.text.isBlank()){
                Toast.makeText(context, getString(R.string.falta_nombre), Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            openSecondActivity()
        }
        //BOTON OBTENER EDAD Y GRUPO - Mostramos texto y activamos el siguiente boton
        binding.buttonObtenerEdadGrupo.setOnClickListener {
            val date = MyDate(studentData[0], studentData[1], studentData[2])
            binding.textViewDatos.text = Utils.getInformationString(date, studentData[3], studentData[4], true)
            binding.buttonGuardar.isEnabled = true
            requireView().hideKeyboard()
        }
        // BOTON GUARDAR - Guardamos el registro y limpiamos los campos
        binding.buttonGuardar.setOnClickListener {
            addRegistry()
            binding.editTextNombreAlu.text.clear()
            binding.textViewDatos.text = ""
            binding.textViewFechaNac.text = getString(R.string.fecha)
            binding.textViewModCiclo.text = getString(R.string.modalidad_ciclo)
            binding.buttonObtenerEdadGrupo.isEnabled = false
            binding.buttonGuardar.isEnabled = false
        }
        //BOTON VER HISTORICO
        //  binding.buttonHistorico.setOnClickListener {
        //     openHistoryActivity()
        //}
    }
    fun openSecondActivity(){
        val myIntent = Intent(context, ActivityAddStudent::class.java).apply {
            putExtra(MainActivity.EXTRA_ALUMNO, binding.editTextNombreAlu.text.toString())
        }
        getResult.launch(myIntent)
    }

    private val getResult = registerForActivityResult(ActivityResultContracts.StartActivityForResult())
    {
        if (it.resultCode == Activity.RESULT_OK) {
            setData(it)
            setTexts()
            binding.buttonObtenerEdadGrupo.isEnabled = true
        }
        if (it.resultCode == Activity.RESULT_CANCELED) {
            Toast.makeText(context, "Cancelado", Toast.LENGTH_SHORT)
        }
    }

    fun setData(it: androidx.activity.result.ActivityResult) {
        val day = it.data!!.getIntExtra(MainActivity.EXTRA_DIA, -1)
        val month = it.data!!.getIntExtra(MainActivity.EXTRA_MES, -1)
        val year = it.data!!.getIntExtra(MainActivity.EXTRA_ANYO, -1)
        val mod = it.data!!.getIntExtra(MainActivity.EXTRA_MODALIDAD, -1)
        val course = it.data!!.getIntExtra(MainActivity.EXTRA_CICLO, -1)
        studentData = arrayOf(day, month, year, mod, course)
        studentName = it.data!!.getStringExtra(MainActivity.EXTRA_ALUMNO).toString()
    }

    //Actualiza los textos correspondientes cuando vuelves de las segunda actividad
    fun setTexts(){
        val date = MyDate(studentData[0], studentData[1], studentData[2])
        binding.textViewFechaNac.text = getString(R.string.fecha_nac_date, date)
        binding.textViewModCiclo.text =
            getString(
                R.string.modalidad_ciclo_2,
                Utils.getMode(studentData[3]),
                Utils.getCiclo(studentData[4])
            )
    }

    //Añade en nuestro archivo el estudiante actual
    private fun addRegistry(){

        val str = "" + studentData[0] + ";" +
                studentData[1] + ";" +
                studentData[2] + ";" +
                binding.editTextNombreAlu.text.toString() + ";" +
                studentData[3] + ";" +
                studentData[4]

        writeInFile(str)
    }

    // Escribe un string en "registry.txt"
    private fun writeInFile(data: String) {
        try {
            val salida = OutputStreamWriter(context?.openFileOutput("registry.txt", AppCompatActivity.MODE_APPEND))
            salida.write(data + '\n')
            salida.flush()
            salida.close()
            Snackbar.make(binding.root, "La información se ha registrado en el histórico", Snackbar.LENGTH_SHORT).show()
        } catch (e: IOException) {
            Toast.makeText(context, e.message, Toast.LENGTH_SHORT).show()
        }
    }
}