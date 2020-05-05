BIN=aliceInJpegLand
SRC=$(addprefix AliceInJpegLand/,\
	Basics.cs ColorExtension.cs Convolution.cs Geometry.cs ParameterManager.cs Program.cs Steganography.cs)
CC=csc               # We use csc and not mcs to have Graphics
FLAGS=-optimize+     # Allows compiler optimization

all: $(BIN)

$(BIN): $(SRC)
	$(CC) $(FLAGS) $^ -out:$@
	@chmod +x $@

clean:
	$(RM) $(BIN)

.PHONY: all clean
