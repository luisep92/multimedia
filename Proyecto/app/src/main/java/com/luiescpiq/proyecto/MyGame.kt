package com.luiescpiq.proyecto

import android.app.AlertDialog
import android.content.Context
import android.content.DialogInterface
import android.view.ActionMode
import android.view.LayoutInflater
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.snackbar.Snackbar
import com.luiescpiq.proyecto.databinding.ItemGameListBinding
import com.squareup.picasso.Picasso

// Luis Escolano Piquer


data class MyGame (val name: String, val genre: String, val description: String, val image: String, val score: Float, val id: String)

class GameAdapter (gameList: MutableList<MyGame>, context: Context): RecyclerView.Adapter<GameAdapter.GameViewHolder>() {
    var myGames: MutableList<MyGame>
    var myContext: Context
    private var actionMode: ActionMode? = null

    init {
        myGames = gameList
        myContext = context
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): GameViewHolder {
        val view = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.item_game_list, parent, false)
        return GameViewHolder(view)
    }
    override fun onBindViewHolder(holder: GameViewHolder, position: Int) {
        val item = myGames.get(position)
        holder.bind(item, myContext)
    }
    override fun getItemCount(): Int {
        return myGames.size
    }

    inner class GameViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        private val binding = ItemGameListBinding.bind(view)
        private lateinit var description: String
        private lateinit var id: String
        private lateinit var image: String
        fun bind(game: MyGame, context: Context) {
            // Rellenamos campos.
            binding.txtNombre.text = game.name
            binding.txtGenero.text = game.genre
            image = game.image
            Picasso.get().load(image).into(binding.image)
            binding.score.rating = game.score
            description = game.description
            id = game.id

            // On click, abrir actividad detalles.
            itemView.setOnClickListener {
                val con = myContext as MainActivity
                con.openDetailsActivity(binding.txtNombre.text.toString(),
                                        binding.txtGenero.text.toString(),
                                        description,
                                        image,
                                        binding.score.rating,
                                        id)
            }

            // Long click listener, abrimos action mode.
            itemView.setOnLongClickListener {
                when (actionMode) {
                    null -> {
                        actionMode = it.startActionMode(actionModeCallback)
                        it.isSelected = true
                        true
                    }
                    else -> false
                }
            }
        }

        // Alert dialog confirmar al borrar
        private fun myAlertDialog(message: String) {
            val builder = AlertDialog.Builder(myContext)
            builder.apply {
                setTitle("Confirmar borrado")
                setMessage(message)
                setPositiveButton("Aceptar") { _, _ ->
                    deleteGame()
                }
                setNegativeButton(android.R.string.cancel) { _, _ ->
                    Toast.makeText(context, "Cancelado", Toast.LENGTH_SHORT).show()
                }
            }
            builder.show()
        }

        // Borrar juego
        fun deleteGame() {
            Database.instance.deleteItem(id)
            notifyItemRemoved(adapterPosition)
            Snackbar.make(binding.root, "Eliminado ${binding.txtNombre.text} correctamente", Snackbar.LENGTH_SHORT).show()
            actionMode!!.finish()
        }

        // Comportamiento action mode.
        private val actionModeCallback = object : ActionMode.Callback {
            override fun onCreateActionMode(mode: ActionMode, menu: Menu): Boolean {
                val inflater = mode?.menuInflater
                inflater?.inflate(R.menu.action_menu, menu)
                return true
            }

            override fun onPrepareActionMode(mode: ActionMode, menu: Menu): Boolean {
                return false
            }

            override fun onActionItemClicked(mode: ActionMode, item: MenuItem): Boolean {
                return when (item.itemId) {
                    // Opción delete.
                    R.id.optionDelete -> {
                        myAlertDialog("¿Seguro que desea eliminar ${binding.txtNombre.text}?")
                        return true
                    }

                    // Opción editar.
                    R.id.optionEdit -> {
                        actionMode?.finish()
                        val con = myContext as MainActivity
                        con.openEditActivity(binding.txtNombre.text.toString(),
                                             binding.txtGenero.text.toString(),
                                             description,
                                             image,
                                             binding.score.rating,
                                             id)
                        return true
                    }
                    else -> false
                }
            }

            override fun onDestroyActionMode(mode: ActionMode) {
                actionMode = null
            }
        }
    }
}