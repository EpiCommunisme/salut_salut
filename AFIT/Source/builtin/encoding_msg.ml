(** Encoding Strings *)

open Builtin
open Basic_arithmetics
open Power

(** Encode a string containing ASCII characters.
    @param str is a string representing message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
 *)
let encode str bits =
  let lg = String.length str in
  let rec encodb str lg i a res = if lg = 0 then res else
      let scal = power 2 bits in
      encodb str (lg-1) (i-1) (scal * a) (Char.code str.[i]*a+res)
        in encodb str lg (lg-1) 1 0;;

(** Decode a string containing ASCII characters.
    @param msg is an integer representing an encoded message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
*)

let rec length l = match l with
  |_::s -> 1+length s
  |_ -> 0 ;;

let deci_to_str int bites =
  let rec dec_r  int str =
  match int with
    |0 -> if String.length str < bites then (let rec troplong str  = match String.length str - bites with
                                           |0 -> str
                                           |_ -> troplong ("0"^str)
                                        in troplong str)
      else str
    |x when x mod 2 = 1 ->  dec_r (x/2) (string_of_int 1 ^str)
    |x  ->  dec_r (x/2) (string_of_int 0 ^ str)
  in dec_r (int) "";;

let str_to_list str  =
  let rec str_r i res=
      match str.[i] with
        |'0' when i = String.length str-1 -> 0::res
        |'1' when i = String.length str-1 -> 1::res
        |'0' -> str_r (i+1) (0::res)
        |'1' -> str_r (i+1) (1::res)
        |_ -> invalid_arg ""
  in str_r 0 [];;

let ledebuthihi l = match l with
    e::l -> e
  |_-> invalid_arg"";;

let list_to_int l bite =
  let rec list_r interl i l =
    match l with
      |e::s when   i mod (bite+1) = 0 -> interl::list_r [e] 2 s
      |e::s -> list_r (e::interl) (i+1) s
      |[] -> interl :: []
  in list_r [] 1 l ;;

let rec bin_to_int l = match l with
  |e ::s -> (let rec bin_r e i res = match e with
      |0::s -> bin_r s (i-1) res
      |1::s -> bin_r s (i-1) (res+ power 2 i)
      |[] -> res
      |_ -> invalid_arg ""
             in bin_r e (length e-1) 0) :: bin_to_int s
  |[] -> [];;

let string_of_char = String.make 1;;

let  list_to_char l =
  let rec ltc_r l res = match l with
  |e::s ->  ltc_r s (string_of_char (char_of_int e) ^ res)
  |[] -> res
  in ltc_r l "";;



let decode msg bits =list_to_char (bin_to_int (list_to_int(str_to_list(deci_to_str msg bits)) bits));;
