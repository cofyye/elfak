#include <stdio.h>
#include <mpi.h>

#define n 3

void main(int argc, char** argv)
{

MPI_Comm cart, comm_dijagonala;
int rank, size, poruka = 0, dims[2] = { n,n }, periods[2] = { 1,1 }, reorder = 0, coords[2], comm_rank;


MPI_Init(&argc, &argv);
MPI_Comm_rank(MPI_COMM_WORLD, &rank);
MPI_Comm_size(MPI_COMM_WORLD, &size);

MPI_Cart_create(MPI_COMM_WORLD, 2, dims, periods, reorder, &cart);
MPI_Cart_coords(cart, rank, 2, coords);

MPI_Comm_split(cart, coords[0] == coords[1], 1, &comm_dijagonala);
MPI_Comm_rank(comm_dijagonala, &comm_rank);

if (coords[0] == coords[1] && comm_dijagonala != MPI_COMM_NULL)
{
if (comm_rank == 0)
poruka = 123;


MPI_Bcast(&poruka, 1, MPI_INT, 0, comm_dijagonala);
printf("Proces:%d sa koordinatama [%d][%d] ima poruku: %d", comm_rank, coords[0], coords[1], poruka);
}

MPI_Finalize();
}