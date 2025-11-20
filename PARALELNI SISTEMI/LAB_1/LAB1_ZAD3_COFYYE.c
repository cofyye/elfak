#include <stdio.h>
#include <string.h>
#include <mpi.h>

int main(int argc, char** argv) {
	int rank = -1, size = -1, root = 0, wRank = -1;

	char poruka1[] = { "Ovo je prva poruka" };
	char poruka2[] = { "Ovo je druga poruka" };
	
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Status status{};

	if (rank == root) {
		for (int i = 1; i < size; i++) {
			MPI_Send(poruka1, strlen(poruka1), MPI_CHAR, i, 0, MPI_COMM_WORLD);
			MPI_Send(poruka2, strlen(poruka2), MPI_CHAR, i, 0, MPI_COMM_WORLD);
			MPI_Recv(&wRank, 1, MPI_INT, i, 1, MPI_COMM_WORLD, &status);

			printf("process %d je dobio identifikator %d\n", rank, wRank);
		}
	}
	else {
		MPI_Recv(poruka1, strlen(poruka2), MPI_CHAR, root, 0, MPI_COMM_WORLD, &status);
		MPI_Recv(poruka2, strlen(poruka2), MPI_CHAR, root, 0, MPI_COMM_WORLD, &status);
		printf("process %d -> %s\n", rank, poruka2);
		printf("process %d -> %s\n", rank, poruka1);
		wRank = rank;
		MPI_Send(&wRank, 1, MPI_INT, root, 1, MPI_COMM_WORLD);
	}

	MPI_Finalize();

	return 0;
}